using System.Net;
using AutoMapper;
using FluentValidation;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Order;
using GreenSphere.Application.Features.Orders.Commands.CreateCashOrder;
using GreenSphere.Application.Features.Orders.Commands.CreateOnlineOrder;
using GreenSphere.Application.Features.Orders.Queries.GetUserOrder;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Enumerations;
using GreenSphere.Domain.Interfaces;
using GreenSphere.Domain.Specifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

namespace GreenSphere.Services.Services;

public sealed class OrderService(
    IMapper mapper,
    IBasketService basketService,
    ICurrentUser currentUser,
    IGenericRepository<Product> productRepository,
    IStringLocalizer<OrderService> localizer,
    IGenericRepository<Order> orderRepository,
    IConfiguration configuration) : IOrderService
{

    private async Task<Result<(List<OrderItem> orderItems, decimal productsTotal)>> CreateOrderAsync()
    {
        var customerBasket = await basketService.GetCustomerBasketAsync();
        var basketItems = customerBasket.Value.Items;

        if (basketItems.Count == 0)
            return Result<(List<OrderItem>, decimal)>.Failure(HttpStatusCode.BadRequest, localizer["EmptyBasket"]);

        var orderItems = new List<OrderItem>();
        var productsTotal = 0m;

        foreach (var basketItem in basketItems)
        {
            var existedProduct = await productRepository.GetByIdAsync(basketItem.ProductId);
            if (existedProduct is null)
                return Result<(List<OrderItem>, decimal)>.Failure(HttpStatusCode.NotFound, localizer["ProductNoLongerAvailable"]);

            var orderItem = new OrderItem
            {
                Id = Guid.NewGuid(),
                PictureUrl = $"{configuration["Urls:BaseApiUrl"]}/Uploads/Images/{existedProduct.Img}",
                UnitPrice = existedProduct.DiscountPercentage.HasValue ?
                    existedProduct.PriceAfterDiscount : existedProduct.OriginalPrice,
                ProductName = existedProduct.Name,
                Quantity = basketItem.Quantity,
                ProductId = existedProduct.Id
            };

            orderItems.Add(orderItem);

            productsTotal += existedProduct.DiscountPercentage.HasValue
                ? (existedProduct.PriceAfterDiscount * basketItem.Quantity)
                : (existedProduct.OriginalPrice * basketItem.Quantity);
        }

        return Result<(List<OrderItem>, decimal)>.Success((orderItems, productsTotal));
    }

    public async Task<Result<OrderDto>> CreateCashOrderAsync(CreateCashOrderCommand command)
    {
        var orderResult = await CreateOrderAsync();

        switch (orderResult.StatusCode)
        {
            case HttpStatusCode.BadRequest:
            case HttpStatusCode.NotFound:
                return Result<OrderDto>.Failure(orderResult.StatusCode, orderResult.Message);
        }

        var orderItems = orderResult.Value.orderItems;

        var deliveryFee = CalculateDeliveryFee(command.Latitude, command.Longitude);

        var order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = currentUser.Id,
            TotalAmount = orderResult.Value.productsTotal + deliveryFee,
            PaymentMethod = PaymentMethod.Cash,
            PaymentStatus = PaymentStatus.Paid,
            OrderStatus = OrderStatus.Shipped,
            PhoneNumber = command.PhoneNumber,
            Latitude = command.Latitude,
            Longitude = command.Longitude,
            DeliveryFee = deliveryFee,
            AdditionalDirections = command.AdditionalDirections,
            AddressLabel = command.AddressLabel,
            BuildingName = command.BuildingName,
            Floor = command.Floor,
            Street = command.Street
        };

        orderItems.ForEach(orderItem => orderItem.OrderId = order.Id);
        order.OrderItems = orderItems;

        orderRepository.Create(order);

        await orderRepository.SaveChangesAsync();

        var mappedOrder = mapper.Map<OrderDto>(order);

        return Result<OrderDto>.Success(mappedOrder);
    }

    public async Task<Result<OrderDto>> CreateOnlineOrderAsync(CreateOnlineOrderCommand command)
    {
        await new CreateOnlineOrderCommandValidator().ValidateAndThrowAsync(command);
        var orderResult = await CreateOrderAsync();
        switch (orderResult.StatusCode)
        {
            case HttpStatusCode.BadRequest:
            case HttpStatusCode.NotFound:
                return Result<OrderDto>.Failure(orderResult.StatusCode, orderResult.Message);
        }

        var orderItems = orderResult.Value.orderItems;

        var deliveryFee = CalculateDeliveryFee(command.Latitude, command.Longitude);

        var order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = currentUser.Id,
            TotalAmount = orderResult.Value.productsTotal + deliveryFee,
            PaymentMethod = PaymentMethod.Card,
            PaymentStatus = PaymentStatus.Pending,
            OrderStatus = OrderStatus.Pending,
            PhoneNumber = command.PhoneNumber,
            Latitude = command.Latitude,
            Longitude = command.Longitude,
            DeliveryFee = deliveryFee,
            AdditionalDirections = command.AdditionalDirections,
            AddressLabel = command.AddressLabel,
            BuildingName = command.BuildingName,
            Floor = command.Floor,
            Street = command.Street
        };

        orderItems.ForEach(orderItem => orderItem.OrderId = order.Id);
        order.OrderItems = orderItems;

        orderRepository.Create(order);

        await orderRepository.SaveChangesAsync();

        var mappedOrder = mapper.Map<OrderDto>(order);

        return Result<OrderDto>.Success(mappedOrder);
    }

    private decimal CalculateDeliveryFee(double latitude, double longitude)
    {
        var storeLatitude = 30.5965; // Menofia Governorate latitude (Shibin El Kom - capital)
        var storeLongitude = 30.9819; // Menofia Governorate longitude (Shibin El Kom - capital)

        var distance = CalculateHaversineDistance(storeLatitude, storeLongitude, latitude, longitude);

        return distance switch
        {
            // 0-5 km
            <= 5 => 20m,
            // 5-10 km
            <= 10 => 30m,
            // 10-20 km
            <= 20 => 50m,
            // 20-50 km (covers nearby governorates)
            <= 50 => 50m + (decimal)Math.Ceiling(distance - 20) * 1.5m,
            _ => 95m + (decimal)Math.Ceiling(distance - 50) * 2m
        };
    }

    private static double CalculateHaversineDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double earthRadius = 6371;

        var dLat = ToRadians(lat2 - lat1);
        var dLon = ToRadians(lon2 - lon1);

        // Haversine formula
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        var distance = earthRadius * c;

        return distance;
    }

    private static double ToRadians(double degrees) => degrees * Math.PI / 180;

    public async Task<Result<IEnumerable<OrderDto>>> GetAllOrdersAsync()
    {
        var specification = new GetAllOrdersWithDetailsSpecification();
        var orders = await orderRepository.GetAllWithSpecificationAsync(specification);
        var mappedOrders = mapper.Map<IEnumerable<OrderDto>>(orders);
        return Result<IEnumerable<OrderDto>>.Success(mappedOrders);
    }

    public async Task<Result<IEnumerable<OrderDto>>> GetUserOrdersAsync()
    {
        var specification = new GetAllOrdersWithDetailsSpecification(currentUser.Id);
        var userOrders = await orderRepository.GetAllWithSpecificationAsync(specification);
        var mappedOrders = mapper.Map<IEnumerable<OrderDto>>(userOrders);
        return Result<IEnumerable<OrderDto>>.Success(mappedOrders);
    }

    public async Task<Result<OrderDto?>> GetUserOrderAsync(GetUserOrderQuery query)
    {
        var specification = new GetUserOrderWithDetailsSpecification(query.OrderId, currentUser.Id);
        var userOrder = await orderRepository.GetBySpecificationAsync(specification);

        return userOrder is null ?
            Result<OrderDto?>.Failure(HttpStatusCode.NotFound) :
            Result<OrderDto?>.Success(mapper.Map<OrderDto>(userOrder));
    }
}
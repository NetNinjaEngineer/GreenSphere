using AutoMapper;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Basket;
using GreenSphere.Application.Features.Basket.Commands.AddItemToBasket;
using GreenSphere.Application.Features.Basket.Commands.RemoveItemFromBasket;
using GreenSphere.Application.Features.Basket.Commands.UpdateItemQuantity;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Interfaces;
using GreenSphere.Domain.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System.Net;

namespace GreenSphere.Services.Services;

public sealed class BasketService(
    ICurrentUser currentUser,
    IMapper mapper,
    IGenericRepository<CustomerBasket> basketRepository,
    IGenericRepository<Product> productRepository,
    IGenericRepository<BasketItem> basketItemRepository,
    IConfiguration configuration,
    IHttpContextAccessor contextAccessor,
    IMemoryCache memoryCache,
    IStringLocalizer<BasketService> localizer) : IBasketService
{
    private readonly string _cacheKey = $"basket_{currentUser.Email}";
    private readonly string _languageCacheKey = $"basket_language_{currentUser.Email}";

    private readonly MemoryCacheEntryOptions _cacheEntryOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7),
        SlidingExpiration = TimeSpan.FromHours(2)
    };

    public async Task<Result<BasketDto>> GetCustomerBasketAsync()
    {
        var acceptLanguage = contextAccessor.HttpContext!.Request.Headers["Accept-Language"].ToString();

        if (memoryCache.TryGetValue(_languageCacheKey, out string? lastLanguage) && lastLanguage != acceptLanguage)
        {
            // Language changed
            memoryCache.Remove(_cacheKey);
            memoryCache.Set(_languageCacheKey, acceptLanguage, _cacheEntryOptions);
        }


        // check cache first
        if (memoryCache.TryGetValue(_cacheKey, out var cachedBasket))
            return Result<BasketDto>.Success((BasketDto)cachedBasket!);

        var customerBasket = await basketRepository.GetBySpecificationAsync(
            specification: new GetCustomerBasketWithItemsSpecification(currentUser.Email));

        if (customerBasket is null)
        {
            customerBasket = new CustomerBasket { Id = Guid.NewGuid(), CustomerEmail = currentUser.Email };
            basketRepository.Create(customerBasket);
            await basketRepository.SaveChangesAsync();
        }

        cachedBasket = mapper.Map<BasketDto>(customerBasket);

        memoryCache.Set(_cacheKey, cachedBasket, _cacheEntryOptions);
        memoryCache.Set(_languageCacheKey, acceptLanguage, _cacheEntryOptions);

        return Result<BasketDto>.Success((BasketDto)cachedBasket!);
    }

    public async Task<Result<BasketDto>> AddItemToCustomerBasketAsync(AddItemToBasketCommand command)
    {
        if (command.Quantity <= 0)
            return Result<BasketDto>.Failure(HttpStatusCode.BadRequest, localizer["QuantityMustBeAtLeastOne"]);

        var existedProduct = await productRepository.GetByIdAsync(command.ProductId);

        if (existedProduct is null)
            return Result<BasketDto>.Failure(HttpStatusCode.NotFound, localizer["ProductNotExists"]);

        var customerBasket = await basketRepository.GetBySpecificationAsync(
            specification: new GetCustomerBasketWithItemsSpecification(currentUser.Email));

        if (customerBasket is null)
            return Result<BasketDto>.Failure(HttpStatusCode.NotFound, localizer["ShoppingCartNotFound"]);

        var specification = new GetBasketItemSpecification(
            customerEmail: currentUser.Email,
            productId: command.ProductId);

        var existedBasketItem = await basketItemRepository.GetBySpecificationAsync(specification);

        if (existedBasketItem is not null)
        {
            existedBasketItem.Quantity += command.Quantity;
            basketItemRepository.Update(existedBasketItem);
        }
        else
        {
            var basketItem = new BasketItem
            {
                Id = Guid.NewGuid(),
                Name = existedProduct.Name,
                CustomerBasketId = customerBasket.Id,
                ImageUrl = contextAccessor.HttpContext!.Request.IsHttps ?
                    $"{configuration["Urls:BaseApiUrl"]}/Uploads/Images/{existedProduct.Img}" :
                    $"{configuration["Urls:FallbackUrl"]}/Uploads/Images/{existedProduct.Img}",
                Price = existedProduct.DiscountPercentage.HasValue
                    ? existedProduct.PriceAfterDiscount
                    : existedProduct.OriginalPrice,
                Quantity = command.Quantity,
                ProductId = existedProduct.Id
            };

            basketItemRepository.Create(basketItem);
        }

        await basketItemRepository.SaveChangesAsync();

        memoryCache.Remove(_cacheKey);

        var updatedBasket = await GetCustomerBasketAsync();

        return Result<BasketDto>.Success(updatedBasket.Value);
    }

    public async Task<Result<int>> GetItemsCountInCustomerBasketAsync()
        => Result<int>.Success((await GetCustomerBasketAsync()).Value.Items.Count());

    public async Task<Result<BasketDto>> RemoveItemFromCustomerBasketAsync(RemoveItemFromBasketCommand command)
    {
        var customerBasket = await basketRepository.GetBySpecificationAsync(
            specification: new GetCustomerBasketWithItemsSpecification(currentUser.Email));

        if (customerBasket is null)
            return Result<BasketDto>.Failure(HttpStatusCode.NotFound, localizer["ShoppingCartNotFound"]);

        var specification = new CheckBasketItemExistedSpecification(
            basketItemId: command.BasketItemId,
            customerEmail: currentUser.Email,
            basketId: customerBasket.Id);

        var existedBasketItem = await basketItemRepository.GetBySpecificationAsync(specification);

        if (existedBasketItem is null)
            return Result<BasketDto>.Failure(HttpStatusCode.NotFound, localizer["ItemNotFoundInCart", command.BasketItemId]);

        basketItemRepository.Delete(existedBasketItem);

        await basketItemRepository.SaveChangesAsync();

        memoryCache.Remove(_cacheKey);

        return Result<BasketDto>.Success((await GetCustomerBasketAsync()).Value);
    }

    public async Task<Result<BasketDto>> UpdateItemQuantityInCustomerBasketAsync(UpdateItemQuantityCommand command)
    {
        if (command.Quantity <= 0)
            return Result<BasketDto>.Failure(HttpStatusCode.BadRequest, localizer["QuantityMustBeAtLeastOne"]);

        var specification = new GetBasketItemSpecification(
            basketItemId: command.BasketItemId,
            customerEmail: currentUser.Email);

        var existedBasketItem = await basketItemRepository.GetBySpecificationAsync(specification);

        if (existedBasketItem is null)
            return Result<BasketDto>.Failure(HttpStatusCode.NotFound, localizer["ItemNotFoundInCart", command.BasketItemId]);

        // for more secure
        var existedProduct = await productRepository.GetByIdAsync(existedBasketItem.ProductId);

        if (existedProduct is null)
            return Result<BasketDto>.Failure(HttpStatusCode.NotFound, localizer["ProductNotExists"]);

        existedBasketItem.Quantity = command.Quantity;

        basketItemRepository.Update(existedBasketItem);

        await basketItemRepository.SaveChangesAsync();

        memoryCache.Remove(_cacheKey);

        return Result<BasketDto>.Success((await GetCustomerBasketAsync()).Value);
    }

    public async Task<Result<bool>> DeleteCustomerBasketAsync()
    {
        var customerBasket = await basketRepository.GetBySpecificationAsync(
            specification: new GetCustomerBasketWithItemsSpecification(currentUser.Email));

        if (customerBasket is null)
            return Result<bool>.Failure(HttpStatusCode.NotFound, localizer["ShoppingCartNotFound"]);

        basketRepository.Delete(customerBasket);

        await basketRepository.SaveChangesAsync();

        memoryCache.Remove(_cacheKey);

        return Result<bool>.Success(true);
    }

    public async Task<Result<BasketDto>> ClearBasketItemsAsync()
    {
        var customerBasket = await basketRepository.GetBySpecificationAsync(
            specification: new GetCustomerBasketWithItemsSpecification(currentUser.Email));

        if (customerBasket is null)
            return Result<BasketDto>.Failure(HttpStatusCode.NotFound, localizer["ShoppingCartNotFound"]);

        customerBasket.BasketItems.Clear();

        basketRepository.Update(customerBasket);

        await basketRepository.SaveChangesAsync();

        memoryCache.Remove(_cacheKey);

        return Result<BasketDto>.Success((await GetCustomerBasketAsync()).Value);
    }
}
using AutoMapper;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Favourite;
using GreenSphere.Application.Features.Favourite.Commands.AddItemToFavourite;
using GreenSphere.Application.Features.Favourite.Commands.RemoveItemFromFavourite;
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

public sealed class FavouriteService(
    ICurrentUser currentUser,
    IMapper mapper,
    IGenericRepository<CustomerFavourite> favouriteRepository,
    IGenericRepository<Product> productRepository,
    IGenericRepository<FavouriteItem> favouriteItemRepository,
    IConfiguration configuration,
    IHttpContextAccessor contextAccessor,
    IMemoryCache memoryCache,
    IStringLocalizer<FavouriteService> localizer) : IFavouriteService
{

    private readonly string _cacheKey = $"favourite_{currentUser.Email}";
    private readonly string _languageCacheKey = $"favourite_language_{currentUser.Email}";

    private readonly MemoryCacheEntryOptions _cacheEntryOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7),
        SlidingExpiration = TimeSpan.FromHours(2)
    };

    public async Task<Result<FavouriteDto>> GetCustomerFavouriteAsync()
    {
        var acceptLanguage = contextAccessor.HttpContext!.Request.Headers["Accept-Language"].ToString();

        if (memoryCache.TryGetValue(_languageCacheKey, out string? lastLanguage) && lastLanguage != acceptLanguage)
        {
            memoryCache.Remove(_cacheKey);
            memoryCache.Set(_languageCacheKey, acceptLanguage, _cacheEntryOptions);
        }

        if (memoryCache.TryGetValue(_cacheKey, out var cachedFavourite))
            return Result<FavouriteDto>.Success((FavouriteDto)cachedFavourite!);


        var customerFavourite = await favouriteRepository.GetBySpecificationAsync(
            specification: new GetCustomerFavouriteWithItemsSpecification(currentUser.Email));

        if (customerFavourite is null)
        {
            customerFavourite = new CustomerFavourite { Id = Guid.NewGuid(), CustomerEmail = currentUser.Email };
            favouriteRepository.Create(customerFavourite);
            await favouriteRepository.SaveChangesAsync();
        }
        cachedFavourite = mapper.Map<FavouriteDto>(customerFavourite);

        memoryCache.Set(_cacheKey, cachedFavourite, _cacheEntryOptions);
        memoryCache.Set(_languageCacheKey, acceptLanguage, _cacheEntryOptions);

        return Result<FavouriteDto>.Success((FavouriteDto)cachedFavourite!);
    }
    public async Task<Result<FavouriteDto>> AddItemToCustomerFavouriteAsync(AddItemToFavouriteCommand command)
    {

        var customerFavourite = await favouriteRepository.GetBySpecificationAsync(
            specification: new GetCustomerFavouriteWithItemsSpecification(currentUser.Email));

        if (customerFavourite is null)
        {
            customerFavourite = new CustomerFavourite
            {
                Id = Guid.NewGuid(),
                CustomerEmail = currentUser.Email
            };
            favouriteRepository.Create(customerFavourite);
        }

        var product = await productRepository.GetByIdAsync(command.ProductId);
        if (product is null)
        {
            return Result<FavouriteDto>.Failure(HttpStatusCode.NotFound, localizer["ProductNotFound"]);
        }

        var existingItem = customerFavourite.FavouriteItems
            .FirstOrDefault(item => item.ProductId == command.ProductId);

        if (existingItem is not null)
        {
            var updatedFavouriteDto = mapper.Map<FavouriteDto>(customerFavourite);
            memoryCache.Set(_cacheKey, updatedFavouriteDto, _cacheEntryOptions);
            return Result<FavouriteDto>.Success(updatedFavouriteDto, localizer["ProductAlreadyInFavourite"]);
        }

        var favouriteItem = new FavouriteItem
        {
            Id = Guid.NewGuid(),
            CustomerFavouriteId = customerFavourite.Id,
            ProductId = command.ProductId
        };

        customerFavourite.FavouriteItems.Add(favouriteItem);
        favouriteItemRepository.Create(favouriteItem);

        await favouriteRepository.SaveChangesAsync();

        var newFavouriteDto = mapper.Map<FavouriteDto>(customerFavourite);
        memoryCache.Set(_cacheKey, newFavouriteDto, _cacheEntryOptions);

        return Result<FavouriteDto>.Success(newFavouriteDto, localizer["ProductAddedToFavourite"]);
    }
    public async Task<Result<FavouriteDto>> RemoveItemFromCustomerFavouriteAsync(RemoveItemFromFavouriteCommand command)
    {
        var customerFavourite = await favouriteRepository.GetBySpecificationAsync(
            specification: new GetCustomerFavouriteWithItemsSpecification(currentUser.Email));

        if (customerFavourite is null)
        {
            return Result<FavouriteDto>.Failure(HttpStatusCode.NotFound, localizer["ItemNotFoundInFavourite"]);
        }

        var itemToRemove = customerFavourite.FavouriteItems
            .FirstOrDefault(item => item.Id == command.ItemId);

        if (itemToRemove is null)
        {
            return Result<FavouriteDto>.Failure(HttpStatusCode.NotFound, localizer["ItemNotFoundInFavourite"]);
        }

        customerFavourite.FavouriteItems.Remove(itemToRemove);
        favouriteItemRepository.Delete(itemToRemove);

        await favouriteRepository.SaveChangesAsync();

        var updatedFavouriteDto = mapper.Map<FavouriteDto>(customerFavourite);
        memoryCache.Set(_cacheKey, updatedFavouriteDto, _cacheEntryOptions);

        return Result<FavouriteDto>.Success(updatedFavouriteDto);
    }
    public async Task<Result<FavouriteDto>> ClearCustomerFavouriteAsync()
    {
        var customerFavourite = await favouriteRepository.GetBySpecificationAsync(
            specification: new GetCustomerFavouriteWithItemsSpecification(currentUser.Email));

        if (customerFavourite is null)
        {
            return Result<FavouriteDto>.Failure(HttpStatusCode.NotFound, localizer["FavouriteNotFound"]);
        }

        foreach (var item in customerFavourite.FavouriteItems.ToList())
        {
            customerFavourite.FavouriteItems.Remove(item);
            favouriteItemRepository.Delete(item);
        }

        await favouriteRepository.SaveChangesAsync();

        var updatedFavouriteDto = mapper.Map<FavouriteDto>(customerFavourite);
        memoryCache.Set(_cacheKey, updatedFavouriteDto, _cacheEntryOptions);

        return Result<FavouriteDto>.Success(updatedFavouriteDto);
    }
    public async Task<Result<FavouriteDto>> DeleteAllCustomerFavouriteAsync()
    {
        var customerFavourite = await favouriteRepository.GetBySpecificationAsync(
            specification: new GetCustomerFavouriteWithItemsSpecification(currentUser.Email));

        if (customerFavourite is null)
        {
            return Result<FavouriteDto>.Failure(HttpStatusCode.NotFound, localizer["FavouriteNotFound"]);
        }

        favouriteRepository.Delete(customerFavourite);

        await favouriteRepository.SaveChangesAsync();

        var emptyFavourite = new CustomerFavourite { Id = Guid.NewGuid(), CustomerEmail = currentUser.Email };
        var updatedFavouriteDto = mapper.Map<FavouriteDto>(emptyFavourite);
        memoryCache.Set(_cacheKey, updatedFavouriteDto, _cacheEntryOptions);

        return Result<FavouriteDto>.Success(updatedFavouriteDto);
    }
}
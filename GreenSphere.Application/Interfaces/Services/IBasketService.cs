using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Basket;
using GreenSphere.Application.Features.Basket.Commands.AddItemToBasket;
using GreenSphere.Application.Features.Basket.Commands.RemoveItemFromBasket;
using GreenSphere.Application.Features.Basket.Commands.UpdateItemQuantity;

namespace GreenSphere.Application.Interfaces.Services;

public interface IBasketService
{
    Task<Result<BasketDto>> GetCustomerBasketAsync();
    Task<Result<BasketDto>> AddItemToCustomerBasketAsync(AddItemToBasketCommand command);
    Task<Result<int>> GetItemsCountInCustomerBasketAsync();
    Task<Result<BasketDto>> RemoveItemFromCustomerBasketAsync(RemoveItemFromBasketCommand command);
    Task<Result<BasketDto>> UpdateItemQuantityInCustomerBasketAsync(UpdateItemQuantityCommand command);
    Task<Result<bool>> DeleteCustomerBasketAsync();
    Task<Result<BasketDto>> ClearBasketItemsAsync();
}
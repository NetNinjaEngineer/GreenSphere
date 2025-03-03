using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Favourite;
using GreenSphere.Application.Features.Favourite.Commands.AddItemToFavourite;
using GreenSphere.Application.Features.Favourite.Commands.RemoveItemFromFavourite;

namespace GreenSphere.Application.Interfaces.Services;

public interface IFavouriteService
{
    Task<Result<FavouriteDto>> GetCustomerFavouriteAsync();
    Task<Result<FavouriteDto>> AddItemToCustomerFavouriteAsync(AddItemToFavouriteCommand command);
    Task<Result<FavouriteDto>> RemoveItemFromCustomerFavouriteAsync(RemoveItemFromFavouriteCommand command); // إضافة الدالة الجديدة
    Task<Result<FavouriteDto>> ClearCustomerFavouriteAsync();
    Task<Result<FavouriteDto>> DeleteAllCustomerFavouriteAsync();
}

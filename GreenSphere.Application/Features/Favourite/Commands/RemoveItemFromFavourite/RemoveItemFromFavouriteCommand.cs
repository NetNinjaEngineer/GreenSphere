using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Favourite;
using MediatR;

namespace GreenSphere.Application.Features.Favourite.Commands.RemoveItemFromFavourite;

public sealed class RemoveItemFromFavouriteCommand : IRequest<Result<FavouriteDto>>
{
    public Guid ItemId { get; set; }
}
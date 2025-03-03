using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Favourite;
using MediatR;

namespace GreenSphere.Application.Features.Favourite.Commands.AddItemToFavourite;

public sealed class AddItemToFavouriteCommand : IRequest<Result<FavouriteDto>>
{
    public Guid ProductId { get; set; }
}

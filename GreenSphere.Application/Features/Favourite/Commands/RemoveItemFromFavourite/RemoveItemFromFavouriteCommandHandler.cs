using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Favourite;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Favourite.Commands.RemoveItemFromFavourite;

public class RemoveItemFromFavouriteCommandHandler(IFavouriteService service)
    : IRequestHandler<RemoveItemFromFavouriteCommand, Result<FavouriteDto>>
{
    public async Task<Result<FavouriteDto>> Handle(
        RemoveItemFromFavouriteCommand request, CancellationToken cancellationToken)
        => await service.RemoveItemFromCustomerFavouriteAsync(request);

}
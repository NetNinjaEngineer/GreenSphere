using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Favourite;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Favourite.Commands.AddItemToFavourite;
public sealed class AddItemToFavouriteCommandHandler(IFavouriteService service)
        : IRequestHandler<AddItemToFavouriteCommand, Result<FavouriteDto>>

{
    public async Task<Result<FavouriteDto>> Handle(
        AddItemToFavouriteCommand request,
        CancellationToken cancellationToken)
        => await service.AddItemToCustomerFavouriteAsync(request);
}
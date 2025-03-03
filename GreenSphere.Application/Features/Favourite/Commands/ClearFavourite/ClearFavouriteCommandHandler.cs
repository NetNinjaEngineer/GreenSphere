using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Favourite;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Favourite.Commands.ClearFavourite;

public class ClearFavouriteCommandHandler(IFavouriteService service)
    : IRequestHandler<ClearFavouriteCommand, Result<FavouriteDto>>
{
    public async Task<Result<FavouriteDto>> Handle(
        ClearFavouriteCommand request, CancellationToken cancellationToken)
        => await service.ClearCustomerFavouriteAsync();

}
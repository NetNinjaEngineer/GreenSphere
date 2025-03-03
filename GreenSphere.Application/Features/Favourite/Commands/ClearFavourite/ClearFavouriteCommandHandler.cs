using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Favourite.Commands.ClearFavourite;

public class ClearFavouriteCommandHandler(IFavouriteService service)
    : IRequestHandler<ClearFavouriteCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(
        ClearFavouriteCommand request, CancellationToken cancellationToken)
        => await service.ClearCustomerFavouriteAsync();

}
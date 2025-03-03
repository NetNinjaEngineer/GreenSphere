using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Favourite;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Favourite.Queries.GetFavourite;
public sealed class GetFavouriteQueryHandler(IFavouriteService service)
: IRequestHandler<GetFavouriteQuery, Result<FavouriteDto>>
{
    public async Task<Result<FavouriteDto>> Handle(
        GetFavouriteQuery request, CancellationToken cancellationToken)
        => await service.GetCustomerFavouriteAsync();

}
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Favourite;
using MediatR;

namespace GreenSphere.Application.Features.Favourite.Queries.GetFavourite;
public sealed class GetFavouriteQuery : IRequest<Result<FavouriteDto>>
{
}
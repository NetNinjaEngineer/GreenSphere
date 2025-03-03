using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Favourite;
using MediatR;

namespace GreenSphere.Application.Features.Favourite.Commands.ClearFavourite;

public sealed class ClearFavouriteCommand : IRequest<Result<FavouriteDto>>
{
}
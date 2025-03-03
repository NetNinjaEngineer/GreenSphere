using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Favourite;
using MediatR;

namespace GreenSphere.Application.Features.Favourite.Commands.DeleteAllFavourite;

public sealed class DeleteAllFavouriteCommand : IRequest<Result<FavouriteDto>>
{
}
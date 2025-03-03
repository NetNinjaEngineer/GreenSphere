using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Favourite.Commands.ClearFavourite;

public sealed class ClearFavouriteCommand : IRequest<Result<bool>>
{
}
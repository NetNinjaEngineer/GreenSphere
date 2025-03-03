using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Favourite.Commands.DeleteAllFavourite;

public sealed class DeleteAllFavouriteCommand : IRequest<Result<bool>>
{
}
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Favourite;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Favourite.Commands.DeleteAllFavourite;

public class DeleteAllFavouriteCommandHandler(IFavouriteService service)
    : IRequestHandler<DeleteAllFavouriteCommand, Result<FavouriteDto>>
{

    public async Task<Result<FavouriteDto>> Handle(DeleteAllFavouriteCommand request, CancellationToken cancellationToken)
      => await service.DeleteAllCustomerFavouriteAsync();

}
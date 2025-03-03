using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Favourite.Commands.DeleteAllFavourite;

public class DeleteAllFavouriteCommandHandler(IFavouriteService service)
    : IRequestHandler<DeleteAllFavouriteCommand, Result<bool>>
{

    public async Task<Result<bool>> Handle(DeleteAllFavouriteCommand request, CancellationToken cancellationToken)
      => await service.DeleteAllCustomerFavouriteAsync();

}
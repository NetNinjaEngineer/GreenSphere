using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.DeleteAddress;

public sealed class DeleteAddressCommandHandler(IAddressService service)
    : IRequestHandler<DeleteAddressCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(
        DeleteAddressCommand request, CancellationToken cancellationToken)
        => await service.DeleteAddressAsync(request.Id);
}
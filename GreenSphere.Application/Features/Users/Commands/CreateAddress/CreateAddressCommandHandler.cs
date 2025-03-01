using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.CreateAddress;

public sealed class CreateAddressCommandHandler(IAddressService service) : IRequestHandler<CreateAddressCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(
        CreateAddressCommand request, CancellationToken cancellationToken)
        => await service.CreateAddressAsync(request);
}
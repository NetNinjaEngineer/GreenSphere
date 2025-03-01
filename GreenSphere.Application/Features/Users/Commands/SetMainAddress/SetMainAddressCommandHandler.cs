using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.SetMainAddress;

public sealed class SetMainAddressCommandHandler(IAddressService service) :
    IRequestHandler<SetMainAddressCommand, Result<AddressDto>>
{
    public async Task<Result<AddressDto>> Handle(
        SetMainAddressCommand request, CancellationToken cancellationToken)
        => await service.SetMainAddressAsync(request.Id);
}
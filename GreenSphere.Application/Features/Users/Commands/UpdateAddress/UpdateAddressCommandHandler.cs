using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.UpdateAddress;

public sealed class UpdateAddressCommandHandler(IAddressService service) :
    IRequestHandler<UpdateAddressCommand, Result<AddressDto>>
{
    public async Task<Result<AddressDto>> Handle(
        UpdateAddressCommand request, CancellationToken cancellationToken)
        => await service.UpdateAddressAsync(request);
}
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Users.Queries.GetAddress;

public sealed class GetAddressQueryHandler(IAddressService service) : IRequestHandler<GetAddressQuery, Result<AddressDto>>
{
    public async Task<Result<AddressDto>> Handle(
        GetAddressQuery request, CancellationToken cancellationToken)
        => await service.GetAddressByIdAsync(request.Id);
}
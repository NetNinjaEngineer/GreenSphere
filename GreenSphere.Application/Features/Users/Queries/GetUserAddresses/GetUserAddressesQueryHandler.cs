using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Users.Queries.GetUserAddresses;

public sealed class GetUserAddressesQueryHandler(IAddressService service) :
    IRequestHandler<GetUserAddressesQuery, Result<List<AddressDto>>>
{
    public async Task<Result<List<AddressDto>>> Handle(
        GetUserAddressesQuery request, CancellationToken cancellationToken)
        => await service.GetUserAddressesAsync();
}
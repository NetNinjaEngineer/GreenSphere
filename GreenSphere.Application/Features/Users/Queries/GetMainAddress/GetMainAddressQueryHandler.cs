using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Users.Queries.GetMainAddress;

public sealed class GetMainAddressQueryHandler(IAddressService service) :
    IRequestHandler<GetMainAddressQuery, Result<AddressDto>>
{
    public async Task<Result<AddressDto>> Handle(
        GetMainAddressQuery request, CancellationToken cancellationToken)
        => await service.GetMainAddressAsync();
}
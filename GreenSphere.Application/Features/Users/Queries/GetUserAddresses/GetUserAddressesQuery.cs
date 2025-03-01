using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using MediatR;

namespace GreenSphere.Application.Features.Users.Queries.GetUserAddresses;

public sealed class GetUserAddressesQuery : IRequest<Result<List<AddressDto>>>
{

}
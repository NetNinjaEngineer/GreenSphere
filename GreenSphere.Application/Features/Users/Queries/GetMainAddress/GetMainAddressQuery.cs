using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using MediatR;

namespace GreenSphere.Application.Features.Users.Queries.GetMainAddress;

public sealed class GetMainAddressQuery : IRequest<Result<AddressDto>>
{

}
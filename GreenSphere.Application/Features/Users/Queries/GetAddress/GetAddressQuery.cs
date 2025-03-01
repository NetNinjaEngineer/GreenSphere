using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using MediatR;

namespace GreenSphere.Application.Features.Users.Queries.GetAddress;

public sealed class GetAddressQuery : IRequest<Result<AddressDto>>
{
    public Guid Id { get; set; }
}
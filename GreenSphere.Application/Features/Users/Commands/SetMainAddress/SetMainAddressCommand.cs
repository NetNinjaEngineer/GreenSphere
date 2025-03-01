using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Users;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.SetMainAddress;

public sealed class SetMainAddressCommand : IRequest<Result<AddressDto>>
{
    public Guid Id { get; set; }
}
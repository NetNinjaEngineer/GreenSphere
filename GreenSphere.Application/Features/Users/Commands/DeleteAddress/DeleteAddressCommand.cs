using GreenSphere.Application.Bases;
using MediatR;

namespace GreenSphere.Application.Features.Users.Commands.DeleteAddress;

public sealed class DeleteAddressCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}
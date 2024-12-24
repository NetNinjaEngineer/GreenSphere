using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Roles.Commands.AssignRoleToUser;

public class AssignRoleToUserCommandHandler(
    IRoleService roleService) : IRequestHandler<AssignRoleToUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        => await roleService.AddRoleToUser(request);
}
using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Roles.Commands.DeleteRole;

public class DeleteRoleCommandHandler(
    IRoleService roleService) : IRequestHandler<DeleteRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
       => await roleService.DeleteRole(request);
}
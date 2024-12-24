using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Roles.Commands.EditRole;

public class EditRoleCommandHandler(IRoleService roleService) : IRequestHandler<EditRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        => await roleService.EditRole(request);
}

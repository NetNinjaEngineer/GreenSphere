using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommandHandler(
    IRoleService roleService) : IRequestHandler<CreateRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        => await roleService.CreateRole(request);
}
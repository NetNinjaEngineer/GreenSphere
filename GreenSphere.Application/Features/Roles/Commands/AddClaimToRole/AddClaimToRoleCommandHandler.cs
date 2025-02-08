using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Roles.Commands.AddClaimToRole;

public class AddClaimToRoleCommandHandler(IRoleService roleService) : IRequestHandler<AddClaimToRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AddClaimToRoleCommand request,
        CancellationToken cancellationToken) => await roleService.AddClaimToRole(request);
}
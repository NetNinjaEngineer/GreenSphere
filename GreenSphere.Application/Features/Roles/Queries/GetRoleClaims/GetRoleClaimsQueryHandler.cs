using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Roles.Queries.GetRoleClaims;

public class GetRoleClaimsQueryHandler(IRoleService roleService) : IRequestHandler<GetRoleClaimsQuery, Result<IEnumerable<string>>>
{
    public async Task<Result<IEnumerable<string>>> Handle(GetRoleClaimsQuery request, CancellationToken cancellationToken)
        => await roleService.GetRoleClaims(request.RoleName);
}
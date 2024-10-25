using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Roles.Requests.Queries;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Roles.Handlers.Queries;
public class RolesQueryHandler(IRoleService roleService) :
    IRequestHandler<GetAllRolesQuery, Result<IEnumerable<string>>>,
    IRequestHandler<GetRoleClaimsQuery, Result<IEnumerable<string>>>,
    IRequestHandler<GetUserClaimsQuery, Result<IEnumerable<string>>>,
    IRequestHandler<GetUserRolesQuery, Result<IEnumerable<string>>>
{
    public async Task<Result<IEnumerable<string>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        => await roleService.GetAllRoles();

    public async Task<Result<IEnumerable<string>>> Handle(GetRoleClaimsQuery request, CancellationToken cancellationToken)
        => await roleService.GetRoleClaims(request.RoleName);

    public async Task<Result<IEnumerable<string>>> Handle(GetUserClaimsQuery request, CancellationToken cancellationToken)
        => await roleService.GetUserClaims(request.UserId);

    public async Task<Result<IEnumerable<string>>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        => await roleService.GetUserRoles(request.UserId);
}

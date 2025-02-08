using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Roles.Queries.GetUserRoles;

public class GetUserRolesQueryHandler(IRoleService roleService) : IRequestHandler<GetUserRolesQuery, Result<IEnumerable<string>>>
{
    public async Task<Result<IEnumerable<string>>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
       => await roleService.GetUserRoles(request.UserId);
}
using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Roles.Queries.GetUserClaims;

public class GetUserClaimsQueryHandler(IRoleService roleService) : IRequestHandler<GetUserClaimsQuery, Result<IEnumerable<string>>>
{
    public async Task<Result<IEnumerable<string>>> Handle(GetUserClaimsQuery request, CancellationToken cancellationToken)
         => await roleService.GetUserClaims(request.UserId);
}

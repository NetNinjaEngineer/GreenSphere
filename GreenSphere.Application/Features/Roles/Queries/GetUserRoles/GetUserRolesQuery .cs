using GreenSphere.Application.Abstractions;
using MediatR;

namespace GreenSphere.Application.Features.Roles.Queries.GetUserRoles;

public class GetUserRolesQuery : IRequest<Result<IEnumerable<string>>>
{
    public string UserId { get; set; } = string.Empty;
}
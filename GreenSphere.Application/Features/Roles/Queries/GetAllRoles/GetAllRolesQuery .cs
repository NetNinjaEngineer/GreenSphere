using GreenSphere.Application.Abstractions;
using MediatR;

namespace GreenSphere.Application.Features.Roles.Queries.GetAllRoles;

public class GetAllRolesQuery : IRequest<Result<IEnumerable<string>>>
{
}

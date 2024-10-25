using GreenSphere.Application.Abstractions;
using MediatR;

namespace GreenSphere.Application.Features.Roles.Requests.Queries;

public class GetAllRolesQuery : IRequest<Result<IEnumerable<string>>>
{
}

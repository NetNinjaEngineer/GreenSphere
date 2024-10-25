using GreenSphere.Application.Abstractions;
using MediatR;

namespace GreenSphere.Application.Features.Roles.Requests.Queries;

public class GetUserClaimsQuery : IRequest<Result<IEnumerable<string>>>
{
    public string UserId { get; set; } = string.Empty;
}

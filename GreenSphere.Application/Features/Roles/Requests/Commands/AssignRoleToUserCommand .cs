using GreenSphere.Application.Abstractions;
using MediatR;

namespace GreenSphere.Application.Features.Roles.Requests.Commands;

public class AssignRoleToUserCommand : IRequest<Result<string>>
{
    public string UserId { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
}

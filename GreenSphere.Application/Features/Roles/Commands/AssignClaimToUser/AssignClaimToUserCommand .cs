using GreenSphere.Application.Abstractions;
using MediatR;

namespace GreenSphere.Application.Features.Roles.Commands.AssignClaimToUser;

public class AssignClaimToUserCommand : IRequest<Result<string>>
{
    public string UserId { get; set; } = string.Empty;
    public string ClaimType { get; set; } = string.Empty;
    public string ClaimValue { get; set; } = string.Empty;
}

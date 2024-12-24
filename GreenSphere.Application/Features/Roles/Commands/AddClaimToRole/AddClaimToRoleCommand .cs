using GreenSphere.Application.Abstractions;
using MediatR;

namespace GreenSphere.Application.Features.Roles.Commands.AddClaimToRole;

public class AddClaimToRoleCommand : IRequest<Result<string>>
{
    public string RoleName { get; set; } = string.Empty;
    public string ClaimType { get; set; } = string.Empty;
    public string ClaimValue { get; set; } = string.Empty;
}

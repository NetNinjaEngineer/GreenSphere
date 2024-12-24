using GreenSphere.Application.Abstractions;
using MediatR;

namespace GreenSphere.Application.Features.Roles.Commands.EditRole;

public class EditRoleCommand : IRequest<Result<string>>
{
    public string RoleName { get; set; } = string.Empty;
    public string NewRoleName { get; set; } = string.Empty;
}
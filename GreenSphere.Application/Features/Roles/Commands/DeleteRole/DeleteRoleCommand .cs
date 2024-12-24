using GreenSphere.Application.Abstractions;
using MediatR;

namespace GreenSphere.Application.Features.Roles.Commands.DeleteRole;

public class DeleteRoleCommand : IRequest<Result<string>>
{
    public string RoleName { get; set; } = string.Empty;
}
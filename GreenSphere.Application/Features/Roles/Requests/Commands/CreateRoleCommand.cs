using GreenSphere.Application.Abstractions;
using MediatR;

namespace GreenSphere.Application.Features.Roles.Requests.Commands;

public class CreateRoleCommand : IRequest<Result<string>>
{
    public string RoleName { get; set; } = string.Empty;
}

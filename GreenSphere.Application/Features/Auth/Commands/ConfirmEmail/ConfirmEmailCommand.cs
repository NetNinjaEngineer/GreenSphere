using GreenSphere.Application.Abstractions;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Commands.ConfirmEmail;
public sealed class ConfirmEmailCommand : IRequest<Result<string>>
{
    public string Provider { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}

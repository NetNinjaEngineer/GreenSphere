using GreenSphere.Application.Abstractions;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Commands.ForgotPassword;

public class ForgotPasswordCommand : IRequest<Result<string>>
{
    public string Provider { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

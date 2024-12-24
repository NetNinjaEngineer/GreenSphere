using GreenSphere.Application.Abstractions;
using GreenSphere.Application.DTOs.Auth;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Commands.Login;

public sealed class LoginCommand : IRequest<Result<SignInResponseDto>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
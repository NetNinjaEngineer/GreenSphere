using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Auth;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Commands.RefreshToken;

public sealed class RefreshTokenCommand : IRequest<Result<SignInResponseDto>>
{
    public string Token { get; set; } = string.Empty;
}
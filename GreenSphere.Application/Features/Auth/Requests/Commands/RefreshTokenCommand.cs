using GreenSphere.Application.Bases;
using GreenSphere.Application.Features.Auth.DTOs;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Requests.Commands;

public sealed class RefreshTokenCommand : IRequest<Result<SignInResponseDto>>
{
    public string Token { get; set; } = string.Empty;
}
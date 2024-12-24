using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Auth;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Commands.ValidateToken;
public sealed class ValidateTokenCommand : IRequest<Result<ValidateTokenResponseDto>>
{
    public string JwtToken { get; set; } = null!;
}

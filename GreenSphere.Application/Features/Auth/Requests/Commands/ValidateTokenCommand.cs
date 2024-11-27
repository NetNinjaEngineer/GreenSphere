using GreenSphere.Application.Bases;
using GreenSphere.Application.Features.Auth.DTOs;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Requests.Commands;
public sealed class ValidateTokenCommand : IRequest<Result<ValidateTokenResponseDto>>
{
    public string JwtToken { get; set; } = null!;
}

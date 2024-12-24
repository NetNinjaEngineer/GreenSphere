using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Auth;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Commands.ValidateToken;
public sealed class ValidateTokenCommandHandler(
    IAuthService authService) : IRequestHandler<ValidateTokenCommand, Result<ValidateTokenResponseDto>>
{
    public async Task<Result<ValidateTokenResponseDto>> Handle(
        ValidateTokenCommand request,
        CancellationToken cancellationToken)
        => await authService.ValidateTokenAsync(request);
}

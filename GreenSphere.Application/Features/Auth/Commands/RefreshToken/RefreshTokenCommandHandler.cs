using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Auth;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler(IAuthService authService) : IRequestHandler<RefreshTokenCommand, Result<SignInResponseDto>>
{
    public async Task<Result<SignInResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        => await authService.RefreshTokenAsync(request);
}
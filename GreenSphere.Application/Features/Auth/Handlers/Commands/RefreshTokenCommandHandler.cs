using GreenSphere.Application.Bases;
using GreenSphere.Application.Features.Auth.DTOs;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Handlers.Commands;

public sealed class RefreshTokenCommandHandler(IAuthService authService) : IRequestHandler<RefreshTokenCommand, Result<SignInResponseDto>>
{
    public Task<Result<SignInResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        => Task.FromResult(authService.RefreshToken(request));
}
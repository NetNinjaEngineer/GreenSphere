using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.DTOs;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Handlers.Commands;
public sealed class Verify2FACodeCommandHandler(IAuthService authService) : IRequestHandler<Verify2FACodeCommand, Result<SignInResponseDto>>
{
    public async Task<Result<SignInResponseDto>> Handle(Verify2FACodeCommand request, CancellationToken cancellationToken)
        => await authService.Verify2FACodeAsync(request);
}

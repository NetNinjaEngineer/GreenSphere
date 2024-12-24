using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Commands.RevokeToken;

public sealed class RevokeTokenCommandHandler(IAuthService authService) : IRequestHandler<RevokeTokenCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        => await authService.RevokeTokenAsync(request);
}
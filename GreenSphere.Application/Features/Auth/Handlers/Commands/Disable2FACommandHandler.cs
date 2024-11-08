using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Handlers.Commands;
public sealed class Disable2FACommandHandler(IAuthService authService) : IRequestHandler<Disable2FACommand, Result<string>>
{
    public async Task<Result<string>> Handle(Disable2FACommand request, CancellationToken cancellationToken)
        => await authService.Disable2FAAsync(request);
}

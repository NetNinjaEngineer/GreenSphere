using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Handlers.Commands;
public sealed class LogoutCommandHandler(IAuthService authService) : IRequestHandler<LogoutCommand>
{
    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        => await authService.LogoutAsync();
}

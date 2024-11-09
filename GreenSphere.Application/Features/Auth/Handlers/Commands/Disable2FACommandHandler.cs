using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Handlers.Commands;

public sealed class Disable2FaCommandHandler(IAuthService authService)
    : IRequestHandler<Disable2FaCommand, Result<string>>
{
    public async Task<Result<string>> Handle(Disable2FaCommand request, CancellationToken cancellationToken)
        => await authService.Disable2FaAsync(request);
}
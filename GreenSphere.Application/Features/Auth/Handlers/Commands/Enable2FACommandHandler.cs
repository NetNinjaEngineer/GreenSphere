using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Handlers.Commands;

public sealed class Enable2FaCommandHandler(IAuthService authService)
    : IRequestHandler<Enable2FaCommand, Result<string>>
{
    public async Task<Result<string>> Handle(Enable2FaCommand request, CancellationToken cancellationToken)
        => await authService.Enable2FaAsync(request);
}
using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Handlers.Commands;

public sealed class ConfirmEnable2FaCommandHandler(IAuthService authService)
    : IRequestHandler<ConfirmEnable2FaCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ConfirmEnable2FaCommand request, CancellationToken cancellationToken)
        => await authService.ConfirmEnable2FaAsync(request);
}
using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Commands.ConfirmEnable2FA;

public sealed class ConfirmEnable2FaCommandHandler(IAuthService authService)
    : IRequestHandler<ConfirmEnable2FaCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ConfirmEnable2FaCommand request, CancellationToken cancellationToken)
        => await authService.ConfirmEnable2FaAsync(request);
}
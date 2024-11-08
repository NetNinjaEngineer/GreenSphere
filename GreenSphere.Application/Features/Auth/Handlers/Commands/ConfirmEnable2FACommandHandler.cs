using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Auth.Requests.Commands;
using GreenSphere.Application.Interfaces.Identity;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Handlers.Commands;
public sealed class ConfirmEnable2FACommandHandler(IAuthService authService) : IRequestHandler<ConfirmEnable2FACommand, Result<string>>
{
    public async Task<Result<string>> Handle(ConfirmEnable2FACommand request, CancellationToken cancellationToken)
       => await authService.ConfirmEnable2FAAsync(request);
}

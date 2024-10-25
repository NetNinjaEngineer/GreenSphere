using GreenSphere.Application.Features.Auth.Requests.Commands;
using MediatR;

namespace GreenSphere.Application.Features.Auth.Handlers.Commands;
public sealed class SendCodeResetPasswordCommandHandler : IRequestHandler<SendCodeResetPasswordCommand, bool>
{
    public Task<bool> Handle(SendCodeResetPasswordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

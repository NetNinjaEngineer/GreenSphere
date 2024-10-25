using MediatR;

namespace GreenSphere.Application.Features.Auth.Requests.Commands;
public sealed class SendCodeResetPasswordCommand : IRequest<bool>
{
}

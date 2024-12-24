using GreenSphere.Application.Abstractions;
using MediatR;

namespace GreenSphere.Application.Features.Email.Commands.SendEmail;
public sealed class SendEmailCommand : IRequest<Result<string>>
{
    public string Provider { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
}

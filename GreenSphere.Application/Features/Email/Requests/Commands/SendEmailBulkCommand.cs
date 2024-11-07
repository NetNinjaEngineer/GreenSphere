using GreenSphere.Application.Abstractions;
using MediatR;

namespace GreenSphere.Application.Features.Email.Requests.Commands;

public sealed class SendEmailBulkCommand : IRequest<Result<string>>
{
    public string Provider { get; set; } = string.Empty;
    public List<string> Recipients { get; set; } = [];
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}
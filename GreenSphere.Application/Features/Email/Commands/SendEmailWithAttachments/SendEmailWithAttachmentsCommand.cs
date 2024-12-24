using GreenSphere.Application.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GreenSphere.Application.Features.Email.Commands.SendEmailWithAttachments;

public sealed class SendEmailWithAttachmentsCommand : IRequest<Result<string>>
{
    public string Provider { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public List<IFormFile> Attachments { get; set; } = [];
}
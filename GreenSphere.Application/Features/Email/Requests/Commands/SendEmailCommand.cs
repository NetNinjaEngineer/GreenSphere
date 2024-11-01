using GreenSphere.Application.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GreenSphere.Application.Features.Email.Requests.Commands;
public sealed class SendEmailCommand : IRequest<Result<string>>
{
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

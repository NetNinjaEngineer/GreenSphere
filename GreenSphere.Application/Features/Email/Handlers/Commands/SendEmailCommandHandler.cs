using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Email.Requests.Commands;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Application.Interfaces.Services.Models;
using MediatR;

namespace GreenSphere.Application.Features.Email.Handlers.Commands;
public sealed class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, Result<string>>
{
    private readonly IMailService _mailService;

    public SendEmailCommandHandler(IMailService mailService)
    {
        _mailService = mailService;
    }

    public async Task<Result<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        => await _mailService.SendEmailAsync(EmailMessage.Create(request.To, request.Subject, request.Message));
}

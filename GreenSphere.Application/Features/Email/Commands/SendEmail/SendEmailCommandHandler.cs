﻿using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Application.Interfaces.Services.Models;
using MediatR;

namespace GreenSphere.Application.Features.Email.Commands.SendEmail;
public sealed class SendEmailCommandHandler(IMailService mailService)
    : IRequestHandler<SendEmailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        => await mailService.SendEmailAsync(new MailkitEmail
        {
            Provider = request.Provider,
            Body = request.Body,
            Subject = request.Subject,
            To = request.To
        });
}

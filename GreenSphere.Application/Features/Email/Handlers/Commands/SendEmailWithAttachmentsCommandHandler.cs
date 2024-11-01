using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Email.Requests.Commands;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Email.Handlers.Commands;

public sealed class SendEmailWithAttachmentsCommandHandler(IMailService mailService) : IRequestHandler<SendEmailWithAttachmentsCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SendEmailWithAttachmentsCommand request,
        CancellationToken cancellationToken)
        => await mailService.SendEmailWithAttachmentsAsync(request);
}
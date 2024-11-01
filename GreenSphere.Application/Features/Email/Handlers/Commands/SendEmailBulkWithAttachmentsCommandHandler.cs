using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Email.Requests.Commands;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Email.Handlers.Commands;

public sealed class SendEmailBulkWithAttachmentsCommandHandler(IMailService mailService) : IRequestHandler<SendEmailBulkWithAttachmentsCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SendEmailBulkWithAttachmentsCommand request, CancellationToken cancellationToken)
        => await mailService.SendEmailToMultipleReceipientsWithAttachmentsAsync(request);
}
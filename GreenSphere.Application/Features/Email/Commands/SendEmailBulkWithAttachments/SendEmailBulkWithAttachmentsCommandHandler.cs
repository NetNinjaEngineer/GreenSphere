using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Email.Commands.SendEmailBulkWithAttachments;

public sealed class SendEmailBulkWithAttachmentsCommandHandler(IMailService mailService) : IRequestHandler<SendEmailBulkWithAttachmentsCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SendEmailBulkWithAttachmentsCommand request, CancellationToken cancellationToken)
        => await mailService.SendEmailToMultipleReceipientsWithAttachmentsAsync(request);
}
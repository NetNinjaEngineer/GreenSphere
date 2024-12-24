using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Email.Commands.SendEmailBulk;

public sealed class SendEmailBulkCommandHandler(IMailService mailService) : IRequestHandler<SendEmailBulkCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SendEmailBulkCommand request, CancellationToken cancellationToken)
        => await mailService.SendEmailToMultipleReceipientsAsync(request);
}
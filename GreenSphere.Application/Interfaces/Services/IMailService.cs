using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Email.Requests.Commands;
using GreenSphere.Application.Interfaces.Services.Models;

namespace GreenSphere.Application.Interfaces.Services;
public interface IMailService
{
    Task<Result<string>> SendEmailAsync(EmailMessage emailMessage);
    Task<Result<string>> SendEmailAsync(MailkitEmail emailMessage);
    Task<Result<string>> SendEmailWithAttachmentsAsync(SendEmailWithAttachmentsCommand command);
    Task<Result<string>> SendEmailToMultipleReceipientsAsync(SendEmailBulkCommand command);
    Task<Result<string>> SendEmailToMultipleReceipientsWithAttachmentsAsync(SendEmailBulkWithAttachmentsCommand command);
}

using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Features.Email.Commands.SendEmailBulk;
using GreenSphere.Application.Features.Email.Commands.SendEmailBulkWithAttachments;
using GreenSphere.Application.Features.Email.Commands.SendEmailWithAttachments;
using GreenSphere.Application.Helpers;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Application.Interfaces.Services.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using MimeKit;

namespace GreenSphere.Services.Services;
public sealed class MailService(
    IOptions<EmailSettings> emailSettingsOptions,
    IStringLocalizer<BaseResponseHandler> localizer)
    : BaseResponseHandler(localizer), IMailService
{
    private readonly EmailSettings _emailSetting = emailSettingsOptions.Value;

    public async Task<Result<string>> SendEmailAsync(MailkitEmail emailMessage)
    {
        var message = CreateMimeMessage(
            GetSmtpSettings(emailMessage.Provider),
            emailMessage.To,
            emailMessage.Subject,
            emailMessage.Body);

        return await SendMessageAsync(emailMessage.Provider, message);
    }

    public async Task<Result<string>> SendEmailWithAttachmentsAsync(SendEmailWithAttachmentsCommand command)
    {
        var message = CreateMimeMessage(
            GetSmtpSettings(command.Provider),
            command.To,
            command.Subject,
            command.Body);

        AddAttachmentsToMessage(message, command.Attachments);
        return await SendMessageAsync(command.Provider, message);
    }

    public async Task<Result<string>> SendEmailToMultipleReceipientsAsync(SendEmailBulkCommand command)
    {
        var message = CreateMimeMessage(
            GetSmtpSettings(command.Provider),
            command.Recipients,
            command.Subject,
            command.Body);

        return await SendMessageAsync(command.Provider, message);
    }

    public async Task<Result<string>> SendEmailToMultipleReceipientsWithAttachmentsAsync(SendEmailBulkWithAttachmentsCommand command)
    {
        var message = CreateMimeMessage(
            GetSmtpSettings(command.Provider),
            command.Receipients,
            command.Subject,
            command.Body);

        AddAttachmentsToMessage(message, command.Attachments);
        return await SendMessageAsync(command.Provider, message);
    }

    private MimeMessage CreateMimeMessage(SmtpSettings smtpSettings, string recipient, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(smtpSettings.SenderName, smtpSettings.SenderEmail));
        message.To.Add(new MailboxAddress("User", recipient));
        message.Subject = subject;

        var builder = new BodyBuilder
        {
            HtmlBody = GenerateHtmlBody(body)
        };

        message.Body = builder.ToMessageBody();
        return message;
    }

    private MimeMessage CreateMimeMessage(SmtpSettings smtpSettings, IEnumerable<string> recipients, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(smtpSettings.SenderName, smtpSettings.SenderEmail));

        foreach (var recipient in recipients)
        {
            message.To.Add(new MailboxAddress("User", recipient));
        }

        message.Subject = subject;

        var builder = new BodyBuilder
        {
            HtmlBody = GenerateHtmlBody(body)
        };

        message.Body = builder.ToMessageBody();
        return message;
    }

    private string GenerateHtmlBody(string bodyContent)
    {
        return $@"
        <html>
            <body style='font-family: Arial, sans-serif;'>
                <div style='max-width: 600px; margin: 0 auto; border: 1px solid #ddd; padding: 20px;'>
                    <div style='text-align: center;'>
                        <img src='' alt='Green Sphere Logo' style='width: 150px; height: auto;' />
                    </div>
                    <div style='margin-top: 20px;'>
                        {bodyContent}
                    </div>
                    <div style='margin-top: 30px; padding-top: 15px; border-top: 1px solid #ddd; font-size: 12px; color: #555; text-align: center;'>
                        <p>© {DateTime.Now.Year} Green Sphere. All rights reserved.</p>
                    </div>
                </div>
            </body>
        </html>";
    }

    private void AddAttachmentsToMessage(MimeMessage message, IEnumerable<IFormFile>? attachments)
    {
        var builder = new BodyBuilder();

        if (attachments != null && attachments.Any())
        {
            foreach (var file in attachments)
            {
                if (file.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    file.CopyTo(memoryStream);
                    builder.Attachments.Add(file.FileName, memoryStream.ToArray(), ContentType.Parse(file.ContentType));
                }
            }
        }

        message.Body = builder.ToMessageBody();
    }

    private async Task<Result<string>> SendMessageAsync(string provider, MimeMessage message)
    {
        if (string.IsNullOrEmpty(provider))
            return BadRequest<string>("Provider can not be empty.");

        var smtpSettings = GetSmtpSettings(provider);

        using var client = new SmtpClient();
        await client.ConnectAsync(smtpSettings.Host, smtpSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(smtpSettings.SenderEmail, smtpSettings.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);

        return Success(Constants.EmailSent);
    }

    private SmtpSettings GetSmtpSettings(string provider)
    {
        SmtpSettings smtpSettings = provider.ToLower() switch
        {
            "gmail" => _emailSetting.Gmail,
            "outlook" => _emailSetting.Outlook,
            _ => throw new ArgumentException("Invalid provider")
        };

        return smtpSettings;
    }
}

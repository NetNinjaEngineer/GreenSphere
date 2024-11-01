using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Helpers;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Application.Interfaces.Services.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using GreenSphere.Application.Features.Email.Requests.Commands;
using Microsoft.AspNetCore.Http;

namespace GreenSphere.Services;
public class MailService(
    IOptions<SendGridSettings> sendGridOptions,
    IOptions<MailkitSettings> mailkitSettingsOptions)
    : BaseResponseHandler, IMailService
{
    private readonly SendGridSettings _sendGridSettings = sendGridOptions.Value;
    private readonly MailkitSettings _mailkitSettings = mailkitSettingsOptions.Value;

    public async Task<Result<string>> SendEmailAsync(EmailMessage emailMessage)
    {
        var client = new SendGridClient(_sendGridSettings.ApiKey);

        var from = new EmailAddress(_sendGridSettings.FromEmail, _sendGridSettings.FromName);

        var to = new EmailAddress(emailMessage.To);

        var htmlBody = $@"
            <html>
                <body style='font-family: Arial, sans-serif;'>
                    <div style='max-width: 600px; margin: 0 auto; border: 1px solid #ddd; padding: 20px;'>
              
                        <div style='text-align: center;'>
                            <img src='' alt='Green Sphere Logo' style='width: 150px; height: auto;' />
                        </div>
   
                        <div style='margin-top: 20px;'>
                            {emailMessage.HtmlBody}
                        </div>
       
                        <div style='margin-top: 30px; padding-top: 15px; border-top: 1px solid #ddd; font-size: 12px; color: #555; text-align: center;'>
                            <p>© {DateTime.Now.Year} Green Sphere. All rights reserved.</p>
                        </div>
                    </div>
                </body>
            </html>";

        var message = MailHelper.CreateSingleEmail(from, to, emailMessage.Subject, null, htmlBody);

        var response = await client.SendEmailAsync(message);

        return IsEmailSent(response) ? Success(Constants.EmailSent) : BadRequest<string>(Constants.EmailNotSent);
    }

    private static bool IsEmailSent(Response response)
        => response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted;
    
    public async Task<Result<string>> SendEmailAsync(MailkitEmail emailMessage)
    {
        var message = CreateMimeMessage(emailMessage.To, emailMessage.Subject, emailMessage.Body);
        return await SendMessageAsync(message);
    }

    public async Task<Result<string>> SendEmailWithAttachmentsAsync(SendEmailWithAttachmentsCommand command)
    {
        var message = CreateMimeMessage(command.To, command.Subject, command.Body);
        AddAttachmentsToMessage(message, command.Attachments);
        return await SendMessageAsync(message);
    }

    public async Task<Result<string>> SendEmailToMultipleReceipientsAsync(SendEmailBulkCommand command)
    {
        var message = CreateMimeMessage(command.Recipients, command.Subject, command.Body);
        return await SendMessageAsync(message);
    }

    public async Task<Result<string>> SendEmailToMultipleReceipientsWithAttachmentsAsync(SendEmailBulkWithAttachmentsCommand command)
    {
        var message = CreateMimeMessage(command.Receipients, command.Subject, command.Body);
        AddAttachmentsToMessage(message, command.Attachments);
        return await SendMessageAsync(message);
    }

    private MimeMessage CreateMimeMessage(string recipient, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_mailkitSettings.SenderName, _mailkitSettings.SenderEmail));
        message.To.Add(new MailboxAddress("User", recipient));
        message.Subject = subject;

        var builder = new BodyBuilder
        {
            HtmlBody = GenerateHtmlBody(body)
        };

        message.Body = builder.ToMessageBody();
        return message;
    }

    private MimeMessage CreateMimeMessage(IEnumerable<string> recipients, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_mailkitSettings.SenderName, _mailkitSettings.SenderEmail));
        
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

    private async Task<Result<string>> SendMessageAsync(MimeMessage message)
    {
        using var client = new SmtpClient();
        await client.ConnectAsync(_mailkitSettings.Host, _mailkitSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_mailkitSettings.SenderEmail, _mailkitSettings.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);

        return Success(Constants.EmailSent);
    }
}

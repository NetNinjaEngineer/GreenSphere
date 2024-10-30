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

namespace GreenSphere.Services;
public class MailService : BaseResponseHandler, IMailService
{
    private readonly SendGridSettings _sendGridSettings;
    private readonly MailkitSettings _mailkitSettings;

    public MailService(IOptions<SendGridSettings> sendGridOptions,
                       IOptions<MailkitSettings> mailkitSettingsOptions)
    {
        _sendGridSettings = sendGridOptions.Value;
        _mailkitSettings = mailkitSettingsOptions.Value;
    }

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
        if (!emailMessage.IsValid())
            return BadRequest<string>("Not Valid Email Message !");

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_mailkitSettings.SenderName, _mailkitSettings.SenderEmail));

        foreach (var toEmail in emailMessage.RecipientEmails)
        {
            message.To.Add(new MailboxAddress("User", toEmail));
        }

        message.Subject = emailMessage.Subject;

        var builder = new BodyBuilder
        {
            TextBody = emailMessage.Body
        };

        message.Body = builder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync(_mailkitSettings.Host, _mailkitSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);

        await client.AuthenticateAsync(_mailkitSettings.SenderEmail, _mailkitSettings.Password);

        await client.SendAsync(message);

        await client.DisconnectAsync(true);

        return Success(Constants.EmailSent);
    }
}

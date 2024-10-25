using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Helpers;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Application.Interfaces.Services.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

namespace GreenSphere.Services;
public class MailService : BaseResponseHandler, IMailService
{
    private readonly SendGridSettings _sendGridSettings;

    public MailService(IOptions<SendGridSettings> sendGridOptions)
    {
        _sendGridSettings = sendGridOptions.Value;
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
}

using GreenSphere.Application.Abstractions;
using GreenSphere.Application.Interfaces.Services.Models;

namespace GreenSphere.Application.Interfaces.Services;
public interface IMailService
{
    Task<Result<string>> SendEmailAsync(EmailMessage emailMessage);
    Task<Result<string>> SendEmailAsync(Email emailMessage);
}

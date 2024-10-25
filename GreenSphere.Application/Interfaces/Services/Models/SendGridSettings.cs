namespace GreenSphere.Application.Interfaces.Services.Models;

public class SendGridSettings
{
    public string FromName { get; set; } = string.Empty;
    public string FromEmail { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
}

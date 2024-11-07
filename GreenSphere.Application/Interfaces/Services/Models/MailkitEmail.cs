namespace GreenSphere.Application.Interfaces.Services.Models;
public sealed class MailkitEmail
{
    public string Provider { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
}

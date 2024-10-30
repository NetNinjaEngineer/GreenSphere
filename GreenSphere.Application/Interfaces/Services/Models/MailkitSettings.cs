namespace GreenSphere.Application.Interfaces.Services.Models;
public sealed class MailkitSettings
{
    public int Port { get; set; }
    public string Host { get; set; } = string.Empty;
    public string SenderName { get; set; } = string.Empty;
    public string SenderEmail { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

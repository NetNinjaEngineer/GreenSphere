namespace GreenSphere.Application.Interfaces.Services.Models;
public class SmtpSettings
{
    public string Host { get; set; } = string.Empty;
    public string SenderName { get; set; } = string.Empty;
    public string SenderEmail { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int Port { get; set; }
}
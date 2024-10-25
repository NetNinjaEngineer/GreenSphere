namespace GreenSphere.Application.Interfaces.Services.Models;
public sealed class EmailMessage
{
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string HtmlBody { get; set; } = string.Empty;
    public string TextBody { get; set; } = string.Empty;

    public static EmailMessage Create(string to, string subject, string message)
        => new() { To = to, Subject = subject, HtmlBody = message };
}

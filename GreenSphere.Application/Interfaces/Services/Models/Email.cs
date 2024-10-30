namespace GreenSphere.Application.Interfaces.Services.Models;
public sealed class Email
{
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public List<string> Attachments { get; set; } = [];
    public List<string> RecipientEmails { get; set; } = [];

    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Subject) &&
              !string.IsNullOrWhiteSpace(Body) &&
              RecipientEmails.Count > 0;
    }
}

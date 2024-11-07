namespace GreenSphere.Application.Interfaces.Services.Models;

public class EmailSettings
{
    public SmtpSettings Gmail { get; set; } = null!;
    public SmtpSettings Outlook { get; set; } = null!;
}
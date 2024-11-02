using Microsoft.AspNetCore.Identity;

namespace GreenSphere.Application.Interfaces.Identity.Entities;
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Code { get; set; }
    public DateTimeOffset? CodeExpiration { get; set; }

    public List<RefreshToken>? RefreshTokens { get; set; }
}

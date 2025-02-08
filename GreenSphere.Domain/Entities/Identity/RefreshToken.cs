using Microsoft.EntityFrameworkCore;

namespace GreenSphere.Domain.Entities.Identity;

[Owned]
public sealed class RefreshToken
{
    public string Token { get; set; } = string.Empty;
    public DateTimeOffset ExpiresOn { get; set; }
    public bool IsExpired => DateTimeOffset.Now >= ExpiresOn;

    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? RevokedOn { get; set; }

    public bool IsActive => RevokedOn == null && !IsExpired;
}
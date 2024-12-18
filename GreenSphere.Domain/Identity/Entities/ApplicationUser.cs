﻿using Microsoft.AspNetCore.Identity;

namespace GreenSphere.Domain.Identity.Entities;
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Code { get; set; }
    public DateTimeOffset? CodeExpiration { get; set; }
    public List<RefreshToken>? RefreshTokens { get; set; }
    public PrivacySetting PrivacySetting { get; set; }
}

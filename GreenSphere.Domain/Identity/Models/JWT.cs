﻿namespace GreenSphere.Domain.Identity.Models;

public sealed class Jwt
{
    public string Key { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public int ExpirationInDays { get; set; }
}
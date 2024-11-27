namespace GreenSphere.Application.Features.Auth.DTOs;
public sealed class ClaimsResponse
{
    public string ClaimType { get; set; } = string.Empty;
    public string ClaimValue { get; set; } = string.Empty;
}
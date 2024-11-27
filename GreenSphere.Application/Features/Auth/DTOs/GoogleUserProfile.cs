namespace GreenSphere.Application.Features.Auth.DTOs;
public sealed record GoogleUserProfile(
    string Email,
    string Name,
    string Picture,
    string FirstName,
    string LastName,
    string GoogleId,
    string Locale,
    bool EmailVerified,
    string HostedDomain,
    TimeSpan Expires
);

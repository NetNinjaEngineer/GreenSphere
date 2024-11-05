namespace GreenSphere.Application.Interfaces.Infrastructure.Models;

public sealed class FacebookAuthenticationResponseDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Picture? Picture { get; set; }
}

namespace GreenSphere.Application.Helpers;
public sealed class JWT
{
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int ExpirationInDays { get; set; }
}

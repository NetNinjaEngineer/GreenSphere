using Newtonsoft.Json;
using System.Text.Json;

namespace GreenSphere.Application.Interfaces.Infrastructure.Models;

public sealed class FacebookAuthenticationResponseDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Picture? Picture { get; set; }
}

public class Picture
{
    [JsonProperty("data")]
    public PictureInfo? Data { get; set; }
}

public sealed class PictureInfo
{
    [JsonProperty("height")]
    public int Height { get; set; }

    [JsonProperty("is_silhouette")]
    public bool IsSilhouette { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; } = string.Empty;

    [JsonProperty("width")]
    public int Width { get; set; }
}
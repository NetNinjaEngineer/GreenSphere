using Newtonsoft.Json;
using System.Text.Json;

namespace GreenSphere.Application.Interfaces.Infrastructure.Models;

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
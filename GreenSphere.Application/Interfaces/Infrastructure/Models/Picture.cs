using Newtonsoft.Json;
using System.Text.Json;

namespace GreenSphere.Application.Interfaces.Infrastructure.Models;

public class Picture
{
    [JsonProperty("data")]
    public PictureInfo? Data { get; set; }
}

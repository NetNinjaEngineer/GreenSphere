using GreenSphere.Application.DTOs.Common;

namespace GreenSphere.Application.DTOs.Ratings;

public sealed class RatingDto : BaseDto
{
    public int Score { get; set; }
    public string? Comment { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public string? ProfilePictureUrl { get; set; }
}
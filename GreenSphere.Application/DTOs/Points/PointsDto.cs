using GreenSphere.Domain.Enumerations;

namespace GreenSphere.Application.DTOs.Points;

public sealed class PointsDto
{
    public Guid Id { get; set; }
    public long Points { get; set; }
    public bool IsSpent { get; set; }
    public bool IsExpired { get; set; }
    public DateTimeOffset EarnedDate { get; set; }
    public DateTimeOffset ExpirationDate { get; set; }
    public string? Description { get; set; }
    public ActivityType ActivityType { get; set; }
}
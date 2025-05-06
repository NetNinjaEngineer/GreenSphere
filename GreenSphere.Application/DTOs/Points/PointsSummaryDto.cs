namespace GreenSphere.Application.DTOs.Points;

public sealed class PointsSummaryDto
{
    public long TotalPoints { get; set; }
    public long AvailablePoints { get; set; }
    public long SpentPoints { get; set; }
    public long ExpiredPoints { get; set; }
    public long ExpiringPoints { get; set; }
    public DateTimeOffset LastEarnedDate { get; set; }
    public DateTimeOffset? NextExpiryDate { get; set; }
}
namespace GreenSphere.Application.DTOs.Rewards;

public class UserRewardDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductImageUrl { get; set; }
    public DateTimeOffset RedeemedDate { get; set; }
    public DateTimeOffset? FulfilledDate { get; set; }
    public long PointsSpent { get; set; }
}
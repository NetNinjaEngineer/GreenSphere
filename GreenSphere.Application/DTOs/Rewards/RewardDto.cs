namespace GreenSphere.Application.DTOs.Rewards;

public class RewardDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long? PointsCost { get; set; }
    public string ImageUrl { get; set; } = null!;
    public bool IsActive { get; set; }
    public long StockQuantity { get; set; }
}
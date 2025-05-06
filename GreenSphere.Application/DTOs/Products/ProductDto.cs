using GreenSphere.Application.DTOs.Category;
using GreenSphere.Application.DTOs.Common;
using GreenSphere.Application.DTOs.Ratings;

namespace GreenSphere.Application.DTOs.Products;
public sealed class ProductDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? DiscountPercentage { get; set; }
    public decimal PriceAfterDiscount { get; set; }
    public double Rate { get; set; }
    public CategoryDto Category { get; set; } = null!;
    public List<RatingDto> Ratings { get; set; } = [];
    public RatingStatisticsDto RatingStatistics { get; set; } = null!;
    public List<RatingDto> RecentRatings { get; set; } = [];
    public long StockQuantity { get; set; }
    public long? PointsCost { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsAvailable { get; set; }
    public bool IsReward { get; set; }
}
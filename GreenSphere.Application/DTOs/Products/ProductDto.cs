using GreenSphere.Application.DTOs.Common;
using GreenSphere.Application.DTOs.Ratings;

namespace GreenSphere.Application.DTOs.Products;
public sealed class ProductDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public decimal? DiscountPercentage { get; set; }
    public double Rate { get; set; }
    public string Category { get; set; } = string.Empty;
    public List<RatingDto> Ratings { get; set; } = [];
}
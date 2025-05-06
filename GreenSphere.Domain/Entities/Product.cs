using GreenSphere.Domain.Common;

namespace GreenSphere.Domain.Entities;

public sealed class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public decimal OriginalPrice { get; set; }
    public string Img { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal? DiscountPercentage { get; set; }
    public double Rate
    {
        get
        {
            if (Ratings.Count == 0) return 0;
            return Math.Round(Ratings.Average(r => r.Score), 1);
        }
    }
    public decimal PriceAfterDiscount
    {
        get
        {
            if (DiscountPercentage.HasValue)
            {
                return OriginalPrice - (OriginalPrice * DiscountPercentage.Value / 100);
            }

            return OriginalPrice;
        }
    }
    public long StockQuantity { get; set; }
    public long? PointsCost { get; set; }
    public bool IsActive { get; set; } = true;
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public ICollection<Rating> Ratings { get; set; } = [];
    public ICollection<ProductTranslation> ProductTranslations { get; set; } = [];
    public bool IsAvailable => StockQuantity > 0 && IsActive;
    public bool IsReward => PointsCost is > 0;
}

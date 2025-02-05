namespace GreenSphere.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public decimal OriginalPrice { get; set; }
        public double Rate { get; set; }
        public string Img { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal? DiscountPercentage { get; set; }
        public decimal PriceAfterDiscount
        {
            get
            {
                if (DiscountPercentage.HasValue)
                {
                    return OriginalPrice - (OriginalPrice * DiscountPercentage.Value / 100);
                }
                else
                {
                    return OriginalPrice;
                }
            }
        }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;

    }
}
using GreenSphere.Domain.Common;

namespace GreenSphere.Domain.Entities;

public sealed class BasketItem : BaseEntity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTimeOffset AddedAt { get; set; } = DateTimeOffset.Now;
    public Guid CustomerBasketId { get; set; }
    public CustomerBasket CustomerBasket { get; set; } = null!;
}
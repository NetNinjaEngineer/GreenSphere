namespace GreenSphere.Application.DTOs.Basket;

public sealed class BasketItemDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid CustomerBasketId { get; set; }
    public DateTimeOffset AddedAt { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice => Quantity * Price;
}
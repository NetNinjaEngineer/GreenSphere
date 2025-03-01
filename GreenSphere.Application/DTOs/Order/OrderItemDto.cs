namespace GreenSphere.Application.DTOs.Order;

public sealed class OrderItemDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string PictureUrl { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
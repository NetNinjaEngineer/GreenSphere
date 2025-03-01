using GreenSphere.Domain.Common;

namespace GreenSphere.Domain.Entities;

public sealed class OrderItem : BaseEntity
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string PictureUrl { get; set; } = null!;
}
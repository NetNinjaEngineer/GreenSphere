using GreenSphere.Domain.Enumerations;

namespace GreenSphere.Application.DTOs.Order;
public sealed class OrderDto
{
    public Guid Id { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string CustomerEmail { get; set; } = null!;
    public DateTimeOffset OrderDate { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public string? PhoneNumber { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DeliveryFee { get; set; }
    public string? BuildingName { get; set; }
    public string? Floor { get; set; }
    public string? Street { get; set; }
    public string? AdditionalDirections { get; set; }
    public string? AddressLabel { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string PaymentIntentId { get; set; } = null!;
    public IEnumerable<OrderItemDto> OrderItems { get; set; } = [];
}
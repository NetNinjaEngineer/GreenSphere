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
    public string? FullName { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? PhoneNumber { get; set; }
    public string? BuildingNumber { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DeliveryFee { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string PaymentIntentId { get; set; } = null!;
    public IEnumerable<OrderItemDto> OrderItems { get; set; } = [];
}
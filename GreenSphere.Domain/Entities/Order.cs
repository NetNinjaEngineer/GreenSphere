using GreenSphere.Domain.Common;
using GreenSphere.Domain.Entities.Identity;
using GreenSphere.Domain.Enumerations;

namespace GreenSphere.Domain.Entities;

public sealed class Order : BaseEntity
{
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public OrderStatus OrderStatus { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public string? PhoneNumber { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DeliveryFee { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? BuildingName { get; set; }
    public string? Floor { get; set; }
    public string? Street { get; set; }
    public string? AdditionalDirections { get; set; }
    public string? AddressLabel { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string? PaymentIntentId { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = [];
}
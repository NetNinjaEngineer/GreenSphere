using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Order;
using MediatR;

namespace GreenSphere.Application.Features.Orders.Commands.CreateOnlineOrder;

public sealed class CreateOnlineOrderCommand : IRequest<Result<OrderDto>>
{
    public string? PhoneNumber { get; set; }
    public string PaymentIntentId { get; set; } = null!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? BuildingName { get; set; }
    public string? Floor { get; set; }
    public string? Street { get; set; }
    public string? AdditionalDirections { get; set; }
    public string? AddressLabel { get; set; }
}
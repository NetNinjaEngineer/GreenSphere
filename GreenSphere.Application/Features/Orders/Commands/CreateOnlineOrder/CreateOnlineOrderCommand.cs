using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Order;
using MediatR;

namespace GreenSphere.Application.Features.Orders.Commands.CreateOnlineOrder;

public sealed class CreateOnlineOrderCommand : IRequest<Result<OrderDto>>
{
    public string? FullName { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? PhoneNumber { get; set; }
    public string? BuildingNumber { get; set; }
    public string PaymentIntentId { get; set; } = null!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
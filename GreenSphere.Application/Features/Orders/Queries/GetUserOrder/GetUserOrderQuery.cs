using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Order;
using MediatR;

namespace GreenSphere.Application.Features.Orders.Queries.GetUserOrder;
public sealed class GetUserOrderQuery : IRequest<Result<OrderDto?>>
{
    public Guid OrderId { get; set; }
}
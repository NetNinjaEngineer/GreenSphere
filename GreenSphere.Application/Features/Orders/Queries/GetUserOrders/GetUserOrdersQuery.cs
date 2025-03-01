using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Order;
using MediatR;

namespace GreenSphere.Application.Features.Orders.Queries.GetUserOrders;
public sealed class GetUserOrdersQuery : IRequest<Result<IEnumerable<OrderDto>>>
{
}
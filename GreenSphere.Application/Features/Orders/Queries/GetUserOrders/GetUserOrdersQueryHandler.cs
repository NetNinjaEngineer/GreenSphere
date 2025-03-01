using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Order;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Orders.Queries.GetUserOrders;
public sealed class GetUserOrdersQueryHandler(IOrderService service)
    : IRequestHandler<GetUserOrdersQuery, Result<IEnumerable<OrderDto>>>
{
    public async Task<Result<IEnumerable<OrderDto>>> Handle(
        GetUserOrdersQuery request, CancellationToken cancellationToken)
        => await service.GetUserOrdersAsync();
}

using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Order;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Orders.Queries.GetUserOrder;

public sealed class GetUserOrderQueryHandler(IOrderService service)
    : IRequestHandler<GetUserOrderQuery, Result<OrderDto?>>
{
    public async Task<Result<OrderDto?>> Handle(
        GetUserOrderQuery request, CancellationToken cancellationToken)
        => await service.GetUserOrderAsync(request);
}

using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Order;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Orders.Queries.GetAll;
public sealed class GetAllQueryHandler(IOrderService service)
    : IRequestHandler<GetAllQuery, Result<IEnumerable<OrderDto>>>
{
    public async Task<Result<IEnumerable<OrderDto>>> Handle(
        GetAllQuery request, CancellationToken cancellationToken)
        => await service.GetAllOrdersAsync();
}
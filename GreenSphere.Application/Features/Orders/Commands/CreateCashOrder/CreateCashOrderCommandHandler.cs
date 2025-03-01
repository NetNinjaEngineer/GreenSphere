using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Order;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Orders.Commands.CreateCashOrder;

public sealed class CreateCashOrderCommandHandler(IOrderService service)
    : IRequestHandler<CreateCashOrderCommand, Result<OrderDto>>
{
    public async Task<Result<OrderDto>> Handle(
        CreateCashOrderCommand request, CancellationToken cancellationToken)
        => await service.CreateCashOrderAsync(request);
}

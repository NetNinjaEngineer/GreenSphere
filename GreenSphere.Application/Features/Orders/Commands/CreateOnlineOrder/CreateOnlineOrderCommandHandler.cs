using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Order;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Orders.Commands.CreateOnlineOrder;
public sealed class CreateOnlineOrderCommandHandler(IOrderService service)
    : IRequestHandler<CreateOnlineOrderCommand, Result<OrderDto>>
{
    public async Task<Result<OrderDto>> Handle(
        CreateOnlineOrderCommand request, CancellationToken cancellationToken)
        => await service.CreateOnlineOrderAsync(request);
}
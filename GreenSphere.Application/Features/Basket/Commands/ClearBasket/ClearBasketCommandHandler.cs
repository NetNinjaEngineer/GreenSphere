using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Basket;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Basket.Commands.ClearBasket;
public sealed class ClearBasketCommandHandler(IBasketService service)
    : IRequestHandler<ClearBasketCommand, Result<BasketDto>>
{
    public async Task<Result<BasketDto>> Handle(
        ClearBasketCommand request, CancellationToken cancellationToken)
        => await service.ClearBasketItemsAsync();
}

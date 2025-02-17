using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Basket;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Basket.Commands.RemoveItemFromBasket;
public sealed class RemoveItemFromBasketCommandHandler(IBasketService service)
    : IRequestHandler<RemoveItemFromBasketCommand, Result<BasketDto>>
{
    public async Task<Result<BasketDto>> Handle(
        RemoveItemFromBasketCommand request, CancellationToken cancellationToken)
        => await service.RemoveItemFromCustomerBasketAsync(request);
}

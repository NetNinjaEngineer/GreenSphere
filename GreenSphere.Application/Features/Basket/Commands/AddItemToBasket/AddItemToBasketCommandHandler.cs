using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Basket;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Basket.Commands.AddItemToBasket;
public sealed class AddItemToBasketCommandHandler(IBasketService service)
    : IRequestHandler<AddItemToBasketCommand, Result<BasketDto>>
{
    public async Task<Result<BasketDto>> Handle(
        AddItemToBasketCommand request,
        CancellationToken cancellationToken)
        => await service.AddItemToCustomerBasketAsync(request);
}

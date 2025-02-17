using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Basket;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Basket.Queries.GetBasket;
public sealed class GetBasketQueryHandler(IBasketService service)
    : IRequestHandler<GetBasketQuery, Result<BasketDto>>
{
    public async Task<Result<BasketDto>> Handle(
        GetBasketQuery request, CancellationToken cancellationToken)
        => await service.GetCustomerBasketAsync();
}

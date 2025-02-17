using GreenSphere.Application.Bases;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Basket.Queries.GetItemsCount;
public sealed class GetItemsCountQueryHandler(IBasketService service)
    : IRequestHandler<GetItemsCountQuery, Result<int>>
{
    public async Task<Result<int>> Handle(GetItemsCountQuery request, CancellationToken cancellationToken)
        => await service.GetItemsCountInCustomerBasketAsync();
}

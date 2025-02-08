using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Products;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Products.Queries.GetProduct;
public sealed class GetProductQueryHandler(IProductsService service)
    : IRequestHandler<GetProductQuery, Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(
        GetProductQuery request, CancellationToken cancellationToken)
        => await service.GetProductAsync(request);
}

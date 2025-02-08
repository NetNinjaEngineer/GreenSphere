using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Products;
using GreenSphere.Application.Interfaces.Services;
using MediatR;

namespace GreenSphere.Application.Features.Products.Queries.GetAllProducts;
public sealed class GetAllProductsQueryHandler(IProductsService service)
    : IRequestHandler<GetAllProductsQuery, Result<IReadOnlyList<ProductDto>>>
{
    public async Task<Result<IReadOnlyList<ProductDto>>> Handle(
        GetAllProductsQuery request, CancellationToken cancellationToken) => await service.GetAllProductsAsync();
}

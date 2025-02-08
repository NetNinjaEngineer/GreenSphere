using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Products;
using MediatR;

namespace GreenSphere.Application.Features.Products.Queries.GetAllProducts;
public sealed class GetAllProductsQuery : IRequest<Result<IReadOnlyList<ProductDto>>>
{
}

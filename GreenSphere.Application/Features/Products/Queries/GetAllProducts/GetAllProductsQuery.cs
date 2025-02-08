using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Products;
using GreenSphere.Domain.Utils;
using MediatR;

namespace GreenSphere.Application.Features.Products.Queries.GetAllProducts;
public sealed class GetAllProductsQuery : IRequest<Result<IReadOnlyList<ProductDto>>>
{
    public ProductSpecParams? ProductSpecParams { get; set; }
}

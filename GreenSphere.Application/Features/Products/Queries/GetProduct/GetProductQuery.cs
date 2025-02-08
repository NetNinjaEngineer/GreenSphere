using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Products;
using MediatR;

namespace GreenSphere.Application.Features.Products.Queries.GetProduct;
public sealed class GetProductQuery : IRequest<Result<ProductDto>>
{
    public Guid Id { get; set; }
}

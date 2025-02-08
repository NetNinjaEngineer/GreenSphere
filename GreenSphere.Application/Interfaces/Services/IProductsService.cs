using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Products;
using GreenSphere.Application.Features.Products.Commands.CreateProduct;
using GreenSphere.Application.Features.Products.Queries.GetProduct;

namespace GreenSphere.Application.Interfaces.Services;

public interface IProductsService
{
    Task<Result<IReadOnlyList<ProductDto>>> GetAllProductsAsync();
    Task<Result<ProductDto>> GetProductAsync(GetProductQuery query);
    Task<Result<Guid>> CreateProductAsync(CreateProductCommand command);
}
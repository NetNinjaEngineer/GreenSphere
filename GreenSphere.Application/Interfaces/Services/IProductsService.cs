using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Products;
using GreenSphere.Application.Features.Products.Commands.CreateProduct;
using GreenSphere.Application.Features.Products.Commands.DeleteProduct;
using GreenSphere.Application.Features.Products.Commands.UpdateProduct;
using GreenSphere.Application.Features.Products.Queries.GetProduct;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Application.Interfaces.Services;

public interface IProductsService
{
    Task<Result<IReadOnlyList<ProductDto>>> GetAllProductsAsync(ProductSpecParams? @params);
    Task<Result<ProductDto>> GetProductAsync(GetProductQuery query);
    Task<Result<Guid>> CreateProductAsync(CreateProductCommand command);
    Task<Result<bool>> DeleteProductAsync(DeleteProductCommand command);
    Task<Result<bool>> UploadProductAsync(UpdateProductCommand command);
}
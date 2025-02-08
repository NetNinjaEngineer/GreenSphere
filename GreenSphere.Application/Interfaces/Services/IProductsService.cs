using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Products;

namespace GreenSphere.Application.Interfaces.Services;

public interface IProductsService
{
    Task<Result<IEnumerable<ProductDto>>> GetAllProductsAsync();
}
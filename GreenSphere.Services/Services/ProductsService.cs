using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Products;
using GreenSphere.Application.Interfaces.Services;

namespace GreenSphere.Services.Services;
public sealed class ProductsService : IProductsService
{
    public async Task<Result<IEnumerable<ProductDto>>> GetAllProductsAsync()
    {
    }
}

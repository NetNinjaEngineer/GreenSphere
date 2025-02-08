using AutoMapper;
using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Products;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Interfaces;
using GreenSphere.Domain.Specifications;

namespace GreenSphere.Services.Services;
public sealed class ProductsService(
    IMapper mapper,
    IGenericRepository<Product> productsRepository) : IProductsService
{
    public async Task<Result<IEnumerable<ProductDto>>> GetAllProductsAsync()
    {
        var specification = new GetAllProductsSpecification();
        var products = await productsRepository.GetAllWithSpecificationAsync(specification);
        var mappedResults = mapper.Map<IEnumerable<ProductDto>>(products);
        return Result<IEnumerable<ProductDto>>.Success(mappedResults);
    }
}

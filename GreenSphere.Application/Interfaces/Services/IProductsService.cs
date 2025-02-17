using GreenSphere.Application.Bases;
using GreenSphere.Application.DTOs.Category;
using GreenSphere.Application.DTOs.Products;
using GreenSphere.Application.Features.Categories.Commands.CreateCategory;
using GreenSphere.Application.Features.Categories.Commands.DeleteCategory;
using GreenSphere.Application.Features.Categories.Commands.UpdateCategory;
using GreenSphere.Application.Features.Categories.Queries.GetCategoryWithProducts;
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



    Task<Result<Guid>> CreatCategoryAsync(CreateCategoryCommand command);
    Task<Result<Guid>> UpdateCategoryAsync(UpdateCategoryCommand command);
    Task<Result<bool>> DeleteCategoryAsync(DeleteCategoryCommand command);
    Task<Result<IReadOnlyList<CategoryDto>>> GetAllCategoriesAsync(CategorySpecParams? @params);
    Task<Result<CategoryWithProductsDto>> GetCategoryWithProductsAsync(Guid categoryId);
}
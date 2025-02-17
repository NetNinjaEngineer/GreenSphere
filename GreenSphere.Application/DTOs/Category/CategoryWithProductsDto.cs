using GreenSphere.Application.DTOs.Products;

namespace GreenSphere.Application.Features.Categories.Queries.GetCategoryWithProducts;

public sealed class CategoryWithProductsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ICollection<ProductDto> Products { get; set; } = [];
}

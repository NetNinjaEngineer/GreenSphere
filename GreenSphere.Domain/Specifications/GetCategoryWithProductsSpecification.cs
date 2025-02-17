using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Specifications;

public sealed class GetCategoryWithProductsSpecification : BaseSpecification<Category>
{
    public GetCategoryWithProductsSpecification(Guid categoryId) : base(c => c.Id == categoryId)
    {
        AddInclude(c => c.Products);
        AddInclude(c => c.Products, p => p.ProductTranslations);
        AddInclude(c => c.CategoryTranslations);
    }
}
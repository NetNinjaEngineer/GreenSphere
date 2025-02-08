using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Specifications;

public sealed class GetProductWithDetailsSpecification : BaseSpecification<Product>
{
    public GetProductWithDetailsSpecification()
    {
        AddInclude(p => p.Category);
        AddInclude(p => p.Ratings);
        AddInclude(p => p.Ratings, r => r.CreatedBy);
        AddInclude(p => p.ProductTranslations);
        AddInclude(p => p.Category.CategoryTranslations);
    }
}
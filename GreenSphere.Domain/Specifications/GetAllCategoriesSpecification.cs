using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Specifications;

public sealed class GetAllCategoriesSpecification : BaseSpecification<Category>
{
    public GetAllCategoriesSpecification(CategorySpecParams? @params)
        : base(c =>
            @params != null && (
            string.IsNullOrEmpty(@params.Search) ||
            c.Name.ToLower().Contains(@params.Search) ||
            (c.Description != null && c.Description.ToLower().Contains(@params.Search))) ||
            c.CategoryTranslations.Any(ct => ct.Name.ToLower().Contains(@params!.Search!)) ||
            c.CategoryTranslations.Any(ct => ct.Description != null && ct.Description.ToLower().Contains(@params!.Search!))
        )
    {
        AddOrderBy(c => c.Name);
        AddInclude(c => c.Products);
        AddInclude(c => c.CategoryTranslations);
    }
}
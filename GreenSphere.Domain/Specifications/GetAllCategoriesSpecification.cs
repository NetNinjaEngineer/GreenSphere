using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Specifications;

public sealed class GetAllCategoriesSpecification : BaseSpecification<Category>
{
    public GetAllCategoriesSpecification(CategorySpecParams? @params)
        : base(c => string.IsNullOrEmpty(@params.Search) ||
        c.Name.ToLower().Contains(@params.Search) ||
        c.Description.ToLower().Contains(@params.Search))
    {
        AddOrderBy(c => c.Name);
        AddInclude(c => c.Products);
    }
}
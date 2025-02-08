using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Specifications;

public sealed class GetAllProductsSpecification : BaseSpecification<Product>
{
    public GetAllProductsSpecification()
    {
        AddInclude(p => p.Category);
        AddInclude(p => p.Ratings);
        AddInclude(p => p.Ratings, r => r.CreatedBy);
    }
}
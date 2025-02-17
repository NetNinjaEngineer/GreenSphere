using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Specifications;

public sealed class GetAllProductsSpecification : BaseSpecification<Product>
{
    public GetAllProductsSpecification(ProductSpecParams? @params)
        : base(p =>
            @params != null && (string.IsNullOrEmpty(@params.Search) ||
                                p.Name.ToLower().Contains(@params.Search.ToLower()) ||
                                p.Description.ToLower().Contains(@params.Search.ToLower()) ||
                                p.ProductTranslations.Any(pt => pt.Name.ToLower().Contains(@params.Search.ToLower())) ||
                                p.ProductTranslations.Any(pt => pt.Description.ToLower().Contains(@params.Search.ToLower())) ||
                                p.Category.Name.ToLower().Contains(@params.Search.ToLower()) ||
                                p.Category.CategoryTranslations.Any(ct => ct.Name.ToLower().Contains(@params.Search.ToLower())) ||
                                p.Category.CategoryTranslations.Any(ct => ct.Description != null && ct.Description.ToLower().Contains(@params.Search.ToLower()))))
    {

        AddInclude(p => p.Category);
        AddInclude(p => p.Ratings);
        AddInclude(p => p.Ratings, r => r.CreatedBy);
        AddInclude(p => p.ProductTranslations);
        AddInclude(p => p.Category.CategoryTranslations);
    }
}
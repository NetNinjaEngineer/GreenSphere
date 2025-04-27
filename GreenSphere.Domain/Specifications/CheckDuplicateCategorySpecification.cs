using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Application.Specifications.ShortCategories;

public class CheckDuplicateCategorySpecification : BaseSpecification<ShortCategory>
{
    public CheckDuplicateCategorySpecification(string nameEn, string nameAr)
        : base(c => c.NameEn == nameEn || c.NameAr == nameAr)
    {
    }
}
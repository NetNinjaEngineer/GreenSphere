using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Specifications;

public class CheckDuplicateCategorySpecification(string nameEn, string nameAr)
    : BaseSpecification<ShortCategory>(c => c.NameEn == nameEn || c.NameAr == nameAr);
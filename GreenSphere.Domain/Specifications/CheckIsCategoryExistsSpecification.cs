using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Specifications;

public sealed class CheckIsCategoryExistsSpecification : BaseSpecification<Category>
{
    public CheckIsCategoryExistsSpecification(string name)
        : base(c => c.Name.ToLower() == name.ToLower())
    {
    }
}

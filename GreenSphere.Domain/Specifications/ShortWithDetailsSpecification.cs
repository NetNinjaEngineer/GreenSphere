using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Specifications;

public sealed class ShortWithDetailsSpecification : BaseSpecification<Short>
{
    public ShortWithDetailsSpecification(Guid id) : base(s => s.Id == id)
    {
        AddInclude(s => s.Creator);
        AddInclude(s => s.ShortCategory);
    }
}
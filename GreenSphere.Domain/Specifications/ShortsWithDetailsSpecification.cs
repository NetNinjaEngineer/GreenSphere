using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Specifications;

public sealed class ShortsWithDetailsSpecification : BaseSpecification<Short>
{
    public ShortsWithDetailsSpecification()
    {
        AddInclude(s => s.Creator);
    }
}
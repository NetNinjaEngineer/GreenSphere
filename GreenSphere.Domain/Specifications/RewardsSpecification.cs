using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Specifications;

public sealed class RewardsSpecification : BaseSpecification<Product>
{
    public RewardsSpecification() : base(p => p.PointsCost != null && p.PointsCost > 0 && p.IsActive && p.StockQuantity > 0)
    {
        AddOrderBy(p => p.PointsCost!);
    }

    public RewardsSpecification(Guid productId) : base(p => p.PointsCost != null && p.PointsCost > 0 && p.Id == productId)
    {
    }
}
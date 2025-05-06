using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Specifications;

public sealed class RewardsSpecification : BaseSpecification<Product>
{
    public RewardsSpecification() : base(p => p.IsReward && p.IsActive && p.StockQuantity > 0)
    {
        AddOrderBy(p => p.PointsCost.HasValue ? p.PointsCost : false);
    }

    public RewardsSpecification(Guid productId) : base(p => p.IsReward && p.Id == productId)
    {
    }
}
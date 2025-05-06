using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Specifications;

public sealed class GetActiveRewardSpecification(Guid productId)
    : BaseSpecification<Product>(p => p.PointsCost != null && p.PointsCost > 0 && p.IsActive && p.StockQuantity > 0 && p.Id == productId);
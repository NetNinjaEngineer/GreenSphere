using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Specifications;

public sealed class GetUserRedeemedRewardsSpecification : BaseSpecification<UserReward>
{
    public GetUserRedeemedRewardsSpecification(string userId) : base(ur => ur.UserId == userId)
    {
        AddInclude(ur => ur.Product);
        AddInclude(ur => ur.Product.ProductTranslations);
        AddOrderByDescending(ur => ur.RedeemedDate);
    }
}
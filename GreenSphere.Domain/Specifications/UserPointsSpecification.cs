using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Specifications;

public sealed class UserPointsWithDetailsSpecification : BaseSpecification<UserPoints>
{
    public UserPointsWithDetailsSpecification(string userId) : base(p => p.UserId == userId)
    {
        AddInclude(p => p.User);
        AddOrderByDescending(p => p.EarnedDate);
    }
}
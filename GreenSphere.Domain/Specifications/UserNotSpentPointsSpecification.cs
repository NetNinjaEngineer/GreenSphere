using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Utils;

namespace GreenSphere.Domain.Specifications;

public sealed class UserNotSpentPointsSpecification : BaseSpecification<UserPoints>
{
    public UserNotSpentPointsSpecification(string userId) : base(p => p.UserId == userId && !p.IsSpent)
    {
        AddOrderBy(p => p.EarnedDate);
    }
}
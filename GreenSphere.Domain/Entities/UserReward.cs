using GreenSphere.Domain.Common;
using GreenSphere.Domain.Entities.Identity;

namespace GreenSphere.Domain.Entities;

public sealed class UserReward : BaseEntity
{
    public string UserId { get; set; } = null!;
    public Guid ProductId { get; set; }
    public DateTimeOffset RedeemedDate { get; set; }
    public DateTimeOffset? FulfilledDate { get; set; }
    public long PointsSpent { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public Product Product { get; set; } = null!;
}

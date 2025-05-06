using GreenSphere.Domain.Common;
using GreenSphere.Domain.Entities.Identity;
using GreenSphere.Domain.Enumerations;

namespace GreenSphere.Domain.Entities;

public sealed class UserPoints : BaseEntity
{
    public long Points { get; set; }
    public DateTimeOffset EarnedDate { get; set; }
    public DateTimeOffset ExpirationDate { get; set; } = DateTimeOffset.Now.AddMonths(1);
    public ActivityType ActivityType { get; set; }
    public string? Description { get; set; }
    public bool IsSpent { get; set; }
    public bool IsExpired => DateTimeOffset.Now > ExpirationDate;
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
}

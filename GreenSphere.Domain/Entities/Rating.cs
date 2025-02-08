using GreenSphere.Domain.Common;
using GreenSphere.Domain.Entities.Identity;

namespace GreenSphere.Domain.Entities;

public sealed class Rating : BaseEntity
{
    public int Score { get; set; }
    public string? Comment { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public string CreatedById { get; set; } = null!;
    public ApplicationUser CreatedBy { get; set; } = null!;
}
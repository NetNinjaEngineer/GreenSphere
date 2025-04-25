using GreenSphere.Domain.Common;
using GreenSphere.Domain.Entities.Identity;

namespace GreenSphere.Domain.Entities;

public sealed class Short : BaseEntity
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string VideoUrl { get; set; } = null!;
    public string? ThumbnailUrl { get; set; }
    public bool IsFeatured { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; }
    public Guid ShortCategoryId { get; set; }
    public ShortCategory ShortCategory { get; set; } = null!;
    public string CreatorId { get; set; } = null!;
    public ApplicationUser Creator { get; set; } = null!;
}
using GreenSphere.Domain.Common;

namespace GreenSphere.Domain.Entities;

public sealed class ShortCategory : BaseEntity
{
    public string NameAr { get; set; } = null!;
    public string NameEn { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; }
    public ICollection<Short> Shorts { get; set; } = [];
}
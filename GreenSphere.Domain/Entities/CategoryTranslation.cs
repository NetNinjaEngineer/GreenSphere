using GreenSphere.Domain.Common;

namespace GreenSphere.Domain.Entities;

public sealed class CategoryTranslation : BaseEntity
{
    public string LanguageCode { get; set; } = null!; // ar-EG
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}
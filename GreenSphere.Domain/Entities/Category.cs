using GreenSphere.Domain.Common;

namespace GreenSphere.Domain.Entities;

public sealed class Category : BaseEntity
{

    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ICollection<Product> Products { get; set; } = [];
    public ICollection<CategoryTranslation> CategoryTranslations { get; set; } = [];
}

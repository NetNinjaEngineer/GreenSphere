using GreenSphere.Domain.Common;

namespace GreenSphere.Domain.Entities;
public sealed class ProductTranslation : BaseEntity
{
    public string LanguageCode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
}
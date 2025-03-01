using GreenSphere.Domain.Common;

namespace GreenSphere.Domain.Entities.Identity;

public sealed class Address : BaseEntity
{
    public string? BuildingName { get; set; }
    public string? Floor { get; set; }
    public string? Street { get; set; }
    public string? AdditionalDirections { get; set; }
    public string? AddressLabel { get; set; }
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
}
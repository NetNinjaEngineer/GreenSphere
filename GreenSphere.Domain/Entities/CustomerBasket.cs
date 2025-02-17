using GreenSphere.Domain.Common;

namespace GreenSphere.Domain.Entities;

public sealed class CustomerBasket : BaseEntity
{
    public string CustomerEmail { get; set; } = null!;
    public ICollection<BasketItem> BasketItems { get; set; } = [];
}
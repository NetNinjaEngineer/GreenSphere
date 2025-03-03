using GreenSphere.Domain.Common;

namespace GreenSphere.Domain.Entities;

public class FavouriteItem : BaseEntity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public decimal Price { get; set; }
    public DateTimeOffset AddedAt { get; set; } = DateTimeOffset.Now;
    public Guid CustomerFavouriteId { get; set; }
    public CustomerFavourite CustomerFavourite { get; set; } = null!;
}

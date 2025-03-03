using GreenSphere.Domain.Common;
using GreenSphere.Domain.Entities;


public sealed class CustomerFavourite : BaseEntity
{
    public string CustomerEmail { get; set; } = null!;
    public ICollection<FavouriteItem> FavouriteItems { get; set; } = [];
}

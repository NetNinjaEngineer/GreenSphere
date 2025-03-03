namespace GreenSphere.Application.DTOs.Favourite;

public class FavouriteItemDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid CustomerFavouiteId { get; set; }
    public DateTimeOffset AddedAt { get; set; }

}

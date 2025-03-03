namespace GreenSphere.Application.DTOs.Favourite;

public sealed class FavouriteDto
{
    public Guid FavouriteId { get; set; }
    public string OwnerEmail { get; set; } = string.Empty;
    public ICollection<FavouriteItemDto> Items { get; set; } = [];
}

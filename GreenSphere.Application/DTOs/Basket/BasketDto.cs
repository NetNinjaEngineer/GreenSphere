namespace GreenSphere.Application.DTOs.Basket;

public sealed class BasketDto
{
    public Guid BasketId { get; set; }
    public string OwnerEmail { get; set; } = string.Empty;
    public ICollection<BasketItemDto> Items { get; set; } = [];
}
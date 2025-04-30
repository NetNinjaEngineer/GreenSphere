using Microsoft.AspNetCore.Http;

namespace GreenSphere.Application.DTOs.Shorts;

public sealed class ShortUpdateDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public IFormFile? Video { get; set; }
    public IFormFile? Thumbnail { get; set; }
    public bool? IsFeatured { get; set; }
    public Guid? ShortCategoryId { get; set; }
}
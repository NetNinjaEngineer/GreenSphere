using GreenSphere.Application.DTOs.Common;

namespace GreenSphere.Application.DTOs.Category;

public sealed class CategoryDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int TotalProducts { get; set; }
}
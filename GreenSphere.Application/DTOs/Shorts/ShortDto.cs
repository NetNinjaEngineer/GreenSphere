namespace GreenSphere.Application.DTOs.Shorts;

public sealed class ShortDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string VideoUrl { get; set; } = null!;
    public string? ThumbnailUrl { get; set; }
    public bool IsFeatured { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string Category { get; set; } = null!;
    public string Creator { get; set; } = null!;
}
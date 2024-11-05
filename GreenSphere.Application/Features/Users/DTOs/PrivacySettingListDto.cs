namespace GreenSphere.Application.Features.Users.DTOs;
public record PrivacySettingListDto
{
    public string ViewProfile { get; set; } = null!;
    public string SendMessages { get; set; } = null!;
    public string ViewActivityStatus { get; set; } = null!;
    public string ViewPosts { get; set; } = null!;
    public string TagInPosts { get; set; } = null!;
}

using GreenSphere.Domain.Enumerations;

namespace GreenSphere.Application.DTOs.Users;
public record AssignPrivacySettingsRequestDto
{
    public PostVisibility PostVisibility { get; set; }
    public ProfileVisibility ProfileVisibility { get; set; }
    public ActivityStatusVisibility ActivityStatusVisibility { get; set; }
    public TaggingPermission TaggingPermission { get; set; }
    public MessagePermission MessagePermission { get; set; }
}

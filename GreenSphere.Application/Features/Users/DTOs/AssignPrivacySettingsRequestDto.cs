using GreenSphere.Domain.Identity.Enumerations;

namespace GreenSphere.Application.Features.Users.DTOs;
public record AssignPrivacySettingsRequestDto
{
    public PostVisibility PostVisibility { get; set; }
    public ProfileVisibility ProfileVisibility { get; set; }
    public ActivityStatusVisibility ActivityStatusVisibility { get; set; }
    public TaggingPermission TaggingPermission { get; set; }
    public MessagePermission MessagePermission { get; set; }
}

using GreenSphere.Domain.Identity.Enumerations;

namespace GreenSphere.Domain.Identity.Entities;
public class PrivacySetting
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public ProfileVisibility ViewProfile { get; set; } = ProfileVisibility.Public;
    public MessagePermission SendMessages { get; set; } = MessagePermission.Everyone;
    public ActivityStatusVisibility ViewActivityStatus { get; set; } = ActivityStatusVisibility.ConnectionsOnly;
    public PostVisibility ViewPosts { get; set; } = PostVisibility.Public;
    public TaggingPermission TagInPosts { get; set; } = TaggingPermission.Everyone;
}

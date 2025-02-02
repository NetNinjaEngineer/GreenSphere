﻿using GreenSphere.Domain.Enumerations;

namespace GreenSphere.Domain.Entities;
public class PrivacySetting
{
    public string Id { get; set; }
    public string UserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public ProfileVisibility ViewProfile { get; set; } = ProfileVisibility.Public;
    public MessagePermission SendMessages { get; set; } = MessagePermission.Everyone;
    public ActivityStatusVisibility ViewActivityStatus { get; set; } = ActivityStatusVisibility.ConnectionsOnly;
    public PostVisibility ViewPosts { get; set; } = PostVisibility.Public;
    public TaggingPermission TagInPosts { get; set; } = TaggingPermission.Everyone;
}

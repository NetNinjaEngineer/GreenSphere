using GreenSphere.Application.Authorization.Requirements.Models;
using Microsoft.AspNetCore.Authorization;

namespace GreenSphere.Application.Authorization.Requirements;
public class PrivacyRequirement(Permission permission) : IAuthorizationRequirement
{
    public Permission Permission { get; set; } = permission;
}

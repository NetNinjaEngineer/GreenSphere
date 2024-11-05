using GreenSphere.Application.Authorization.Requirements;
using GreenSphere.Application.Authorization.Requirements.Models;
using GreenSphere.Application.Interfaces.Identity;
using GreenSphere.Domain.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GreenSphere.Application.Authorization.Handlers;
public sealed class PrivacyAuthorizationHandler(IUserPrivacyService privacyService) : AuthorizationHandler<PrivacyRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PrivacyRequirement requirement)
    {
        var userId = context.User.FindFirstValue(CustomClaimTypes.Uid);
        var settings = privacyService.GetUserPrivacySettingsAsync(userId!).Result.Value;

        if (settings == null)
            return Task.CompletedTask;

        if (requirement.Permission == Permission.CanViewProfile && settings.ViewProfile == "Public")
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}

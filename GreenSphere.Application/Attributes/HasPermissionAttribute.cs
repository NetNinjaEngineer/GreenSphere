using System.Security.Claims;
using GreenSphere.Application.Authorization.Requirements.Models;
using GreenSphere.Application.Interfaces.Identity;
using GreenSphere.Domain.Identity.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace GreenSphere.Application.Attributes;

public class HasPermissionAttribute(Permission permission) : Attribute, IAsyncAuthorizationFilter
{
    private readonly Permission _permission = permission;

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {     
        // get current user id to get his privacy settings
        var currentUserId = context.HttpContext.User.FindFirstValue(CustomClaimTypes.Uid);
        // get privacy service
        var privacyService = context.HttpContext.RequestServices.GetRequiredService<IUserPrivacyService>();
        var settings = await privacyService.GetUserPrivacySettingsAsync(currentUserId!);
        // check permissions
        if (!(settings.Value.ViewProfile.Equals("public", StringComparison.CurrentCultureIgnoreCase) && _permission == Permission.CanViewProfile))
        {
            context.Result = new ForbidResult();
        }
        
    }
}
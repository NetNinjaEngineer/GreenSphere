using GreenSphere.Application.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GreenSphere.Application.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class AccessDeniedResponseFilter : Attribute, IAsyncAuthorizationFilter
{
    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity!.IsAuthenticated)
            context.Result = new UnauthorizedObjectResult(
                Response.Unauthorized());

        else if (context.HttpContext.Response.StatusCode == StatusCodes.Status403Forbidden)
            context.Result = new ObjectResult(
                Response.Forbiden());

        return Task.CompletedTask;
    }
}

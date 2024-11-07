using GreenSphere.Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GreenSphere.Application.Filters;
public sealed class RequestGuardFilter(
    IAuthorizationService authorizationService,
    string[] policies,
    params string[] roles) : IAsyncAuthorizationFilter
{
    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (!user.Identity?.IsAuthenticated ?? true)
        {
            context.Result = new UnauthorizedObjectResult(Response.Unauthorized());
            return Task.CompletedTask;
        }


        if (roles.Length > 0 && !roles.Any(user.IsInRole))
        {
            context.Result = new ObjectResult(Response.Forbiden());

            return Task.CompletedTask;
        }


        var policyAuthorizationTasks = policies.Select(
                policy => authorizationService.AuthorizeAsync(user, policy)
            ).ToArray();

        return Task.WhenAll(policyAuthorizationTasks)
           .ContinueWith(tasks =>
           {
               if (tasks.Result.Any(x => !x.Succeeded))
               {
                   context.Result = new ObjectResult(Response.Forbiden());
               }
           });
    }
}

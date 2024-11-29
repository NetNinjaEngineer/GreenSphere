using GreenSphere.Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GreenSphere.Application.Filters;

public sealed class GuardFilter(
    IAuthorizationService authorizationService,
    string[]? policies = null,
    string[]? roles = null) : IAsyncAuthorizationFilter
{
    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity!.IsAuthenticated)
        {
            context.Result = new UnauthorizedObjectResult(GetUnauthorizedResponse());

            return Task.CompletedTask;
        }

        if (context.HttpContext.Response.StatusCode == StatusCodes.Status403Forbidden)
        {
            context.Result = new ObjectResult(GetForbidenResponse());

            return Task.CompletedTask;
        }

        var user = context.HttpContext.User;

        if (roles?.Length > 0 && !roles.Any(user.IsInRole))
        {
            context.Result = new ObjectResult(GetForbidenResponse());

            return Task.CompletedTask;
        }

        if (policies?.Length == 0) return Task.CompletedTask;

        var authTasks = policies.Select(
            policy => authorizationService.AuthorizeAsync(user, policy));

        return Task.WhenAll(authTasks)
            .ContinueWith(authorizationTasks =>
            {
                if (authorizationTasks.Result.Any(authResult => !authResult.Succeeded))
                    context.Result = new ObjectResult(GetForbidenResponse());
            });

    }

    private static GlobalErrorResponse GetUnauthorizedResponse()
    {
        return new GlobalErrorResponse()
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unauthorized",
            Detail =
                "Authentication is required to access this resource. Please ensure you are logged in with appropriate credentials."
        };
    }

    private static GlobalErrorResponse GetForbidenResponse()
    {
        return new GlobalErrorResponse()
        {
            Status = StatusCodes.Status403Forbidden,
            Title = "Forbidden",
            Detail = "You do not have permission to access this resource."
        };
    }
}
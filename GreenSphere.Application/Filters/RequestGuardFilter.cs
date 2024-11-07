using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GreenSphere.Application.Filters;
public sealed class RequestGuardFilter(
    IAuthorizationService authorizationService,
    string[]? policies = null,
    params string[] roles) : IAsyncAuthorizationFilter
{
    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        ProblemDetails problemDetails = new();

        if (!user.Identity!.IsAuthenticated)
        {
            problemDetails.Status = StatusCodes.Status401Unauthorized;
            problemDetails.Title = "Unauthorized";
            problemDetails.Detail = "Authentication is required to access this resource. Please ensure you are logged in with appropriate credentials.";

            context.Result = new UnauthorizedObjectResult(problemDetails);
            return Task.CompletedTask;
        }

        foreach (var role in roles)
        {
            if (!user.IsInRole(role))
            {
                SetForbidenAuthResult(problemDetails);

                context.Result = new ObjectResult(problemDetails);
            }
        }

        if (policies?.Length > 0)
        {
            foreach (var userPolicy in policies)
            {
                var authResult = authorizationService.AuthorizeAsync(user, userPolicy).Result;
                if (!authResult.Succeeded)
                {
                    SetForbidenAuthResult(problemDetails);
                    context.Result = new ObjectResult(problemDetails);
                    return Task.CompletedTask;
                }

            }
        }

        return Task.CompletedTask;
    }

    private static void SetForbidenAuthResult(ProblemDetails problemDetails)
    {
        problemDetails.Status = StatusCodes.Status403Forbidden;
        problemDetails.Title = "Forbidden";
        problemDetails.Detail = "You do not have permission to access this resource.";
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GreenSphere.Application.Filters;
public sealed class RequestGuardFilter(string role) : IAsyncAuthorizationFilter
{
    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        ProblemDetails problemDetails = new();

        if (!context.HttpContext.User.Identity!.IsAuthenticated)
        {
            problemDetails.Status = StatusCodes.Status401Unauthorized;
            problemDetails.Title = "Unauthorized";
            problemDetails.Detail = "Authentication is required to access this resource. Please ensure you are logged in with appropriate credentials.";

            context.Result = new UnauthorizedObjectResult(problemDetails);
            return Task.CompletedTask;
        }

        if (!context.HttpContext.User.IsInRole(role))
        {
            problemDetails.Status = StatusCodes.Status403Forbidden;
            problemDetails.Title = "Forbidden";
            problemDetails.Detail = "You do not have permission to access this resource.";

            context.Result = new ObjectResult(problemDetails);
        }

        return Task.CompletedTask;
    }
}

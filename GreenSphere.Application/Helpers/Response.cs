using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Application.Helpers;
public static class Response
{
    public static ProblemDetails CreateProblemDetails(int status, string title, string detail) => new()
    {
        Status = status,
        Title = title,
        Detail = detail
    };

    public static ProblemDetails Unauthorized() => new()
    {
        Status = 401,
        Title = "Unauthorized",
        Detail = "Authentication is required to access this resource. Please ensure you are logged in with appropriate credentials."
    };

    public static ProblemDetails Forbiden() => new()
    {
        Status = 403,
        Title = "Forbidden",
        Detail = "You do not have permission to access this resource."
    };
}

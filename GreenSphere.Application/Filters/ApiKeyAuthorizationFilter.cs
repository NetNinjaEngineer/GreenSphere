using GreenSphere.Application.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace GreenSphere.Application.Filters;
public class ApiKeyAuthorizationFilter(IConfiguration configuration) : IAuthorizationFilter
{
    private const string ApiKeyHeaderName = "X-API-Key";

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var apiKey = context.HttpContext.Request.Headers[ApiKeyHeaderName];
        if (!CheckIsValidApiKey(apiKey!))
        {
            context.Result = new UnauthorizedObjectResult(
                Response.CreateProblemDetails(
                    StatusCodes.Status401Unauthorized,
                    "Unauthorized",
                    "API Key required to access the endpoints. API Key is sent as a request header."
                    ));
        }
    }

    private bool CheckIsValidApiKey(string apiKey)
    {
        if (string.IsNullOrEmpty(apiKey))
            return false;

        return apiKey == configuration["ApiKey"];
    }
}
using GreenSphere.Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace GreenSphere.Application.Filters;
public class ApiKeyAuthorizationFilter : IAuthorizationFilter
{
    private readonly string ApiKeyHeaderName = "X-API-Key";
    private readonly IConfiguration _configuration;

    public ApiKeyAuthorizationFilter(IConfiguration configuration) => _configuration = configuration;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var apiKey = context.HttpContext.Request.Headers[ApiKeyHeaderName];
        if (!CheckIsValidApiKey(apiKey!))
        {
            context.Result = new UnauthorizedObjectResult(new UnAuthorizedApiKeyResponse { StatusCode = HttpStatusCode.Unauthorized, Message = "Api Key is required." });
            return;
        }
    }

    private bool CheckIsValidApiKey(string apiKey)
    {
        if (string.IsNullOrEmpty(apiKey))
            return false;

        return apiKey == _configuration["ApiKey"];
    }
}

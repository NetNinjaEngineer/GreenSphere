using GreenSphere.Application.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace GreenSphere.Api.Middlewares;

public sealed class JwtValidationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var jwtToken = context.Request.Headers.Authorization.ToString().Replace("Bearer ", string.Empty);

        if (!string.IsNullOrEmpty(jwtToken))
        {
            try
            {
                // check for each request if the token is used from different location or any other device
                // using ip address and user-agent
                // check fingerprint based on ip address and user-agent

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.ReadJwtToken(jwtToken);

                var ipAddressClaim = securityToken.Claims?.FirstOrDefault(c => c.Type == CustomClaimTypes.IP)?.Value;
                var userAgentClaim = securityToken.Claims?.FirstOrDefault(c => c.Type == CustomClaimTypes.UserAgent)?.Value;
                var fingerPrintClaim = securityToken.Claims?.FirstOrDefault(c => c.Type == CustomClaimTypes.FingerPrint)?.Value;

                var currentIpAddress = context.Connection.RemoteIpAddress?.ToString();
                var currentUserAgent = context.Request.Headers.UserAgent.ToString();
                var currentFingerPrint = Convert.ToBase64String(
                    SHA256.HashData(Encoding.UTF8.GetBytes(string.Concat(currentIpAddress, currentUserAgent))));

                if (ipAddressClaim != currentIpAddress || userAgentClaim != currentUserAgent
                    || fingerPrintClaim != currentFingerPrint)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;

                    await context.Response.WriteAsJsonAsync(
                        new GlobalErrorResponse
                        {
                            Type = "Forbidden",
                            Status = StatusCodes.Status403Forbidden,
                            Message = "Access denied."
                        });

                    return;
                }
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new GlobalErrorResponse
                {
                    Type = "Bad Request",
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Token validation failed."
                });
                return;
            }

        }

        await next(context);
    }
}

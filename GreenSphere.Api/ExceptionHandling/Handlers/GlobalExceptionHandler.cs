using FluentValidation;
using Google.Apis.Auth;
using GreenSphere.Application.Helpers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace GreenSphere.Api.ExceptionHandling.Handlers;

internal sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger,
    IWebHostEnvironment environment) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(
            exception,
            "Error occurred while processing request: {Message}",
            exception.Message);

        var errorResponse = new GlobalErrorResponse();

        switch (exception)
        {
            case ValidationException validationException:
                httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                errorResponse.Type = "Validation_Error";
                errorResponse.Message = "One or more validation errors occurred.";
                errorResponse.Errors = validationException.Errors
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                break;

            case UnauthorizedAccessException unauthorizedException:
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                errorResponse.Type = "Unauthorized";
                errorResponse.Message = "You are not authorized to perform this action.";
                break;

            case BadHttpRequestException badRequestException:
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                errorResponse.Type = "Bad_Request";
                errorResponse.Message = badRequestException.Message;
                break;

            case InvalidOperationException invalidOpException:
                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                errorResponse.Type = "Invalid_Operation";
                errorResponse.Message = invalidOpException.Message;
                break;

            case DbUpdateException dbUpdateException:
                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                errorResponse.Type = "Database_Error";
                errorResponse.Message = "A database error occurred while processing your request.";
                errorResponse.Detail = environment.IsDevelopment()
                    ? dbUpdateException.InnerException?.Message
                    : null;
                break;

            case InvalidJwtException invalidJwtException:
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                errorResponse.Type = "Google_Authentication_Error";
                errorResponse.Message = "Invalid jwt";
                errorResponse.Detail = invalidJwtException.Message;
                break;

            default:
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                errorResponse.Type = "Internal_Server_Error";
                errorResponse.Message = "An unexpected error occurred while processing your request.";
                errorResponse.Detail = exception.Message;
                break;
        }

        httpContext.Response.ContentType = "application/json";

        await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

        return true;
    }

}

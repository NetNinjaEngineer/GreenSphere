using FluentValidation;
using System.Net;
using System.Text.Json;

namespace GreenSphere.Api.Middlewares;

public class GlobalErrorHandingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(ex, context);
        }
    }

    private static async Task HandleExceptionAsync(Exception ex, HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        JsonErrorResponse? errorResponse = new();
        switch (ex)
        {
            case ValidationException validationException:
                var validationErrors = validationException.Errors
                    .Select(x => x.ErrorMessage)
                    .ToList();

                errorResponse.Errors = validationErrors;
                errorResponse.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                errorResponse.Message = "Validation Errors";
                break;

            default:
                errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Message = "Server Error";
                errorResponse.Errors = [ex.Message];
                break;
        }

        context.Response.StatusCode = errorResponse.StatusCode;
        await context.Response.WriteAsync(errorResponse.ToString());
    }

    internal class JsonErrorResponse
    {
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public IEnumerable<string> Errors { get; set; } = [];
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}

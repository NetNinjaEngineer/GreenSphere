using FluentValidation;
using GreenSphere.Application.Helpers;
using Microsoft.AspNetCore.Mvc;
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
        ProblemDetails errorResponse;
        switch (ex)
        {
            case ValidationException validationException:
                var validationError = validationException.Errors
                    .Select(x => x.ErrorMessage)
                    .ToList();

                errorResponse = Response.CreateProblemDetails(
                    status: (int)HttpStatusCode.UnprocessableEntity,
                    title: "validation errors",
                    detail: string.Join(" || ", validationError));

                break;

            default:
                errorResponse = Response.CreateProblemDetails(
                     status: (int)HttpStatusCode.InternalServerError,
                     title: "server error",
                     detail: ex.Message);
                break;
        }

        context.Response.StatusCode = errorResponse.Status ?? 500;
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}

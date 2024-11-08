using System.Net;

namespace GreenSphere.Application.Abstractions;

public class BaseResponseHandler
{
    public static Result<T> Deleted<T>()
    {
        return new SuccessResult<T>()
        {
            StatusCode = HttpStatusCode.NoContent,
            Succeeded = true,
            Message = "Deleted Successfully"
        };
    }

    public static Result<T> Success<T>(T entity)
    {
        return new SuccessResult<T>()
        {
            Data = entity,
            StatusCode = HttpStatusCode.OK,
            Succeeded = true,
            Message = "Success",
        };
    }

    public static Result<T> Success<T>(T entity, string message)
    {
        return new SuccessResult<T>()
        {
            Data = entity,
            StatusCode = HttpStatusCode.OK,
            Succeeded = true,
            Message = message,
        };
    }


    public static Result<T> Unauthorized<T>()
    {
        return new FailedResult<T>()
        {
            StatusCode = HttpStatusCode.Unauthorized,
            Succeeded = false,
            Message = "UnAuthorized"
        };
    }

    public static Result<T> Unauthorized<T>(string message)
    {
        return new FailedResult<T>()
        {
            StatusCode = HttpStatusCode.Unauthorized,
            Succeeded = false,
            Message = string.IsNullOrEmpty(message) ? "Unauthorized" : message
        };
    }


    public static Result<T> BadRequest<T>(string message, List<string> errors = null)
    {
        return new FailedResult<T>()
        {
            StatusCode = HttpStatusCode.BadRequest,
            Succeeded = false,
            Message = string.IsNullOrWhiteSpace(message) ? "BadRequest" : message,
            Errors = errors
        };
    }

    public static Result<T> Conflict<T>(string message = null)
    {
        return new FailedResult<T>()
        {
            StatusCode = HttpStatusCode.Conflict,
            Succeeded = false,
            Message = string.IsNullOrWhiteSpace(message) ? "Conflict" : message
        };
    }

    public static Result<T> UnprocessableEntity<T>(string message = null)
    {
        return new FailedResult<T>()
        {
            StatusCode = HttpStatusCode.UnprocessableEntity,
            Succeeded = false,
            Message = string.IsNullOrWhiteSpace(message) ? "Unprocessable Entity" : message
        };
    }

    public static Result<T> NotFound<T>(string message = null)
    {
        return new FailedResult<T>()
        {
            StatusCode = HttpStatusCode.NotFound,
            Succeeded = false,
            Message = string.IsNullOrWhiteSpace(message) ? "Not Found" : message
        };
    }

    public static Result<T> Created<T>(T entity)
    {
        return new SuccessResult<T>()
        {
            Data = entity,
            StatusCode = HttpStatusCode.Created,
            Succeeded = true,
            Message = "Created"
        };
    }

    public Result<T> Created<T>()
    {
        return new SuccessResult<T>()
        {
            Data = default!,
            StatusCode = HttpStatusCode.Created,
            Succeeded = true,
            Message = "Created"
        };
    }
}

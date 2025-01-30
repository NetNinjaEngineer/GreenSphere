using Microsoft.Extensions.Localization;
using System.Net;

namespace GreenSphere.Application.Abstractions;

public class BaseResponseHandler
{
    protected readonly IStringLocalizer<BaseResponseHandler> _localizer;

    public BaseResponseHandler(IStringLocalizer<BaseResponseHandler> localizer)
    {
        _localizer = localizer;
    }

    public Result<T> Deleted<T>()
    {
        return new SuccessResult<T>()
        {
            StatusCode = HttpStatusCode.NoContent,
            Succeeded = true,
            Message = _localizer["DeletedSuccessfully"]
        };
    }

    public Result<T> Success<T>(T entity)
    {
        return new SuccessResult<T>()
        {
            Data = entity,
            StatusCode = HttpStatusCode.OK,
            Succeeded = true,
            Message = _localizer["Successfully"],
        };
    }

    public Result<T> Success<T>(T entity, string message)
    {
        return new SuccessResult<T>()
        {
            Data = entity,
            StatusCode = HttpStatusCode.OK,
            Succeeded = true,
            Message = message,
        };
    }


    public Result<T> Unauthorized<T>()
    {
        return new FailedResult<T>()
        {
            StatusCode = HttpStatusCode.Unauthorized,
            Succeeded = false,
            Message = _localizer["UnAuthorized"]
        };
    }

    public Result<T> Unauthorized<T>(string message)
    {
        return new FailedResult<T>()
        {
            StatusCode = HttpStatusCode.Unauthorized,
            Succeeded = false,
            Message = string.IsNullOrEmpty(message) ? "Unauthorized" : message
        };
    }


    public Result<T> BadRequest<T>(string message, List<string> errors = null)
    {
        return new FailedResult<T>()
        {
            StatusCode = HttpStatusCode.BadRequest,
            Succeeded = false,
            Message = string.IsNullOrWhiteSpace(message) ? _localizer["BadRequest"] : message,
            Errors = errors
        };
    }

    public Result<T> Conflict<T>(string message = null)
    {
        return new FailedResult<T>()
        {
            StatusCode = HttpStatusCode.Conflict,
            Succeeded = false,
            Message = string.IsNullOrWhiteSpace(message) ? _localizer["Conflict"] : message
        };
    }

    public Result<T> UnprocessableEntity<T>(string message = null)
    {
        return new FailedResult<T>()
        {
            StatusCode = HttpStatusCode.UnprocessableEntity,
            Succeeded = false,
            Message = string.IsNullOrWhiteSpace(message) ? _localizer["UnprocessableEntity"] : message
        };
    }

    public Result<T> NotFound<T>(string message = null)
    {
        return new FailedResult<T>()
        {
            StatusCode = HttpStatusCode.NotFound,
            Succeeded = false,
            Message = string.IsNullOrWhiteSpace(message) ? _localizer["NotFound"] : message
        };
    }

    public Result<T> Created<T>(T entity)
    {
        return new SuccessResult<T>()
        {
            Data = entity,
            StatusCode = HttpStatusCode.Created,
            Succeeded = true,
            Message = _localizer["Created"]
        };
    }

    public Result<T> Created<T>()
    {
        return new SuccessResult<T>()
        {
            Data = default!,
            StatusCode = HttpStatusCode.Created,
            Succeeded = true,
            Message = _localizer["Created"]
        };
    }
}

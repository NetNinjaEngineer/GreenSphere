namespace GreenSphere.Application.Bases;

using System.Collections.Generic;
using System.Net;

public class Result<TSuccess>
{
    public TSuccess Value { get; set; }
    public bool IsSuccess { get; set; }
    public bool IsFailure => !IsSuccess;
    public string Message { get; set; }
    public List<string>? Errors { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    // success result
    private Result(TSuccess value, string? message = null)
    {
        Value = value;
        IsSuccess = true;
        Message = string.IsNullOrEmpty(message) ? string.Empty : message;
        StatusCode = HttpStatusCode.OK;
    }

    // failure result
    private Result(HttpStatusCode statusCode, string? failureMessage = null, List<string>? errors = null)
    {
        Value = default!;
        IsSuccess = false;
        Message = string.IsNullOrEmpty(failureMessage) ? string.Empty : failureMessage;
        Errors = errors?.Count > 0 ? errors : [];
        StatusCode = statusCode;
    }

    public static Result<TSuccess> Success(TSuccess value, string? successMessage = null) => new(value, successMessage);
    public static Result<TSuccess> Failure(HttpStatusCode statusCode, string error) => new(statusCode, error);
    public static Result<TSuccess> Failure(HttpStatusCode statusCode, string? message = null, List<string>? errors = null) => new(statusCode, message, errors);

    public Result<TNextSuccess> Bind<TNextSuccess>(Func<TSuccess, Result<TNextSuccess>> next)
    {
        return IsSuccess ? next(Value) : Result<TNextSuccess>.Failure(StatusCode, Message, Errors);
    }

    public Result<TNext> Map<TNext>(Func<TSuccess, TNext> mapper)
    {
        return IsSuccess ? Result<TNext>.Success(mapper(Value)) : Result<TNext>.Failure(StatusCode, Message, Errors);
    }
}
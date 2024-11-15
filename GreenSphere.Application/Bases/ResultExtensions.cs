using System.Net;

namespace GreenSphere.Application.Bases;
public static class ResultExtensions
{
    public static Result<T> ToResult<T>(this T value) =>
        value != null ? Result<T>.Success(value) : Result<T>.Failure(HttpStatusCode.NotFound, "Value is null");

    public static async Task<Result<T>> ToResultAsync<T>(this Task<T> task)
    {
        try
        {
            var result = await task;
            return result.ToResult();
        }
        catch (Exception ex)
        {
            return Result<T>.Failure(HttpStatusCode.BadRequest, ex.Message);
        }
    }

    public static Result<TResult> Map<T, TResult>(
        this Result<T> result,
        Func<T, TResult> map) =>
        result.IsSuccess ? Result<TResult>.Success(map(result.Value)) : Result<TResult>.Failure(HttpStatusCode.BadRequest, result.Message);

    public static async Task<Result<TResult>> MapAsync<T, TResult>(
        this Result<T> result,
        Func<T, Task<TResult>> mapAsync) =>
        result.IsSuccess ? Result<TResult>.Success(await mapAsync(result.Value)) : Result<TResult>.Failure(HttpStatusCode.BadRequest, result.Message);

    public static Result<T> Tap<T>(this Result<T> result, Action<T> action)
    {
        if (result.IsSuccess)
            action(result.Value);
        return result;
    }

    public static async Task<Result<T>> TapAsync<T>(
        this Result<T> result,
        Func<T, Task> actionAsync)
    {
        if (result.IsSuccess)
            await actionAsync(result.Value);
        return result;
    }
}
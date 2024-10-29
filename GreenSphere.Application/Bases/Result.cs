namespace GreenSphere.Application.Bases;
public class Result<TSuccess>
{
    public TSuccess Value { get; set; }
    public bool IsSuccess { get; set; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; set; } = string.Empty;

    protected Result(TSuccess value, string error, bool isSuccess)
    {
        Value = value;
        Error = error;
        IsSuccess = isSuccess;
    }

    public static Result<TSuccess> Success(TSuccess value) => new(value, null!, true);

    public static Result<TSuccess> Failure(string error) => new(default!, error, false);

    public Result<TNextSuccess> Bind<TNextSuccess>(Func<TSuccess, Result<TNextSuccess>> next)
    {
        return IsSuccess ? next(Value) : Result<TNextSuccess>.Failure(Error);
    }

    public Result<TNext> Map<TNext>(Func<TSuccess, TNext> mapper)
    {
        return IsSuccess ? Result<TNext>.Success(mapper(Value)) : Result<TNext>.Failure(Error);
    }
}

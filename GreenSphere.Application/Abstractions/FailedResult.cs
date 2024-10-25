namespace GreenSphere.Application.Abstractions;

public class FailedResult<T> : Result<T>
{
    public List<string> Errors { get; set; } = [];
}
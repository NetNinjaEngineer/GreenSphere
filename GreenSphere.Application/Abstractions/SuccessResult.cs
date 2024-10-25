namespace GreenSphere.Application.Abstractions;

public class SuccessResult<T> : Result<T>
{
    public T Data { get; set; }
}

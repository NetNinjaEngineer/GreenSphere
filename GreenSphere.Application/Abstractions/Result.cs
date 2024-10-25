using System.Net;

namespace GreenSphere.Application.Abstractions;
public abstract class Result<T>
{
    public HttpStatusCode StatusCode { get; set; }
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
}

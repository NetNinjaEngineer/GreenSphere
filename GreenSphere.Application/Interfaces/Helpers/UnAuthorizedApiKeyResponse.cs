using System.Net;

namespace GreenSphere.Application.Helpers;

public class UnAuthorizedApiKeyResponse
{
    public string Message { get; set; } = string.Empty;
    public HttpStatusCode StatusCode { get; set; }
}

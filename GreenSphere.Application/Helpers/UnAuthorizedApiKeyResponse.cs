namespace GreenSphere.Application.Helpers;

public class UnAuthorizedApiKeyResponse
{
    public string Status { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public int StatusCode { get; set; }
}

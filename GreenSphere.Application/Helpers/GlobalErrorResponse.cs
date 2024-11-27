using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Application.Helpers;
public sealed class GlobalErrorResponse : ProblemDetails
{
    public string? Message { get; set; }
    public IEnumerable<string> Errors { get; set; } = [];
}

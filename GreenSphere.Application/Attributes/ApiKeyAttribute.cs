using GreenSphere.Application.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Application.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAttribute : ServiceFilterAttribute
{
    public ApiKeyAttribute() : base(typeof(ApiKeyAuthorizationFilter))
    {
    }
}
using GreenSphere.Application.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Application.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AccessDeniedResponseAttribute : ServiceFilterAttribute
{
    public AccessDeniedResponseAttribute() : base(typeof(AccessDeniedResponseFilter))
    {
    }
}
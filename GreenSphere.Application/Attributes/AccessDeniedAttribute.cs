using GreenSphere.Application.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Application.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AccessDeniedAttribute : ServiceFilterAttribute
{
    public AccessDeniedAttribute() : base(typeof(AccessDeniedFilter))
    {
        Order = 2;
    }
}
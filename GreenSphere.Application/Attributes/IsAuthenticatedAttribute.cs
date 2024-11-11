using GreenSphere.Application.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Application.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class IsAuthenticatedAttribute : ServiceFilterAttribute
{
    public IsAuthenticatedAttribute() : base(typeof(IsAuthenticatedFilter))
    {
        Order = 2;
    }
}
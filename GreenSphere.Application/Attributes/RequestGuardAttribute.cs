using GreenSphere.Application.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Application.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class RequestGuardAttribute : TypeFilterAttribute
{
    public RequestGuardAttribute(string role) : base(typeof(RequestGuardFilter))
    {
        Arguments = [role];
        Order = 3;
    }
}

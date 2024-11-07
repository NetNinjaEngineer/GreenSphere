using GreenSphere.Application.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Application.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class RequestGuardAttribute : TypeFilterAttribute
{
    public RequestGuardAttribute(string[]? policies = null, params string[] roles) : base(typeof(RequestGuardFilter))
    {
        Arguments = [policies ?? [], roles];
        Order = 3;
    }
}

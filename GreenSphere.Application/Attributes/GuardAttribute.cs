using GreenSphere.Application.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Application.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class GuardAttribute : TypeFilterAttribute
{
    public GuardAttribute(string[]? policies = null, params string[] roles) : base(typeof(GuardFilter))
    {
        Arguments = [policies ?? [], roles];
        Order = 3;
    }
}
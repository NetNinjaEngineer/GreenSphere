using GreenSphere.Application.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GreenSphere.Application.Attributes;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class GuardAttribute : TypeFilterAttribute
{
    public GuardAttribute(
        string[]? policies = null,
        string[]? roles = null) : base(typeof(GuardFilter))
    {
        Arguments = [policies ?? [], roles ?? []];
        Order = 2;
    }
}

using GreenSphere.Application.Interfaces.Identity;
using GreenSphere.Application.Interfaces.Identity.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GreenSphere.Identity.Services;
public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUser(IHttpContextAccessor contextAccessor) => _contextAccessor = contextAccessor;

    public string Id => _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid)!;
}

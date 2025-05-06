using System.Security.Claims;
using GreenSphere.Application.Helpers;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GreenSphere.Services.Services;
public sealed class CurrentUser(
    UserManager<ApplicationUser> userManager,
    IHttpContextAccessor contextAccessor) : ICurrentUser
{
    public string Id => contextAccessor.HttpContext?.User.FindFirstValue(CustomClaimTypes.Uid)!;
    public string Email => contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email)!;
    public ClaimsPrincipal? GetUser() => contextAccessor.HttpContext?.User;

    public async Task<bool> IsExistsAsync(string email) =>
        await userManager.Users.AnyAsync(u => u.Email == email);
}

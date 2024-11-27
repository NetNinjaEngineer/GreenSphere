using GreenSphere.Domain.Identity.Entities;
using System.Security.Claims;

namespace GreenSphere.Application.Interfaces.Identity;
public interface ITokenService
{
    Task<string> GenerateJwtTokenAsync(ApplicationUser user);
    Task<ClaimsPrincipal> ValidateToken(string token);
}

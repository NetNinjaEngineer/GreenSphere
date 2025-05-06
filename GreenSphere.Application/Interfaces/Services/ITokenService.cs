using System.Security.Claims;
using GreenSphere.Domain.Entities.Identity;

namespace GreenSphere.Application.Interfaces.Services;
public interface ITokenService
{
    Task<string> GenerateJwtTokenAsync(ApplicationUser user);
    Task<ClaimsPrincipal> ValidateToken(string token);
}
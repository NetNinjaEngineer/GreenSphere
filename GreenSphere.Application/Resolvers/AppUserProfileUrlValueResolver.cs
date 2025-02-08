using AutoMapper;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace GreenSphere.Application.Resolvers;

public sealed class AppUserProfileUrlValueResolver(
    IConfiguration configuration,
    IHttpContextAccessor contextAccessor) : IValueResolver<ApplicationUser, UserProfileDto, string?>
{
    public string? Resolve(ApplicationUser source, UserProfileDto destination, string? destMember, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source.ProfilePictureUrl))
            return string.Empty;

        return contextAccessor.HttpContext!.Request.IsHttps
            ? $"{configuration["Urls:BaseApiUrl"]}/Uploads/Images/{source.ProfilePictureUrl}"
            : $"{configuration["Urls:FallbackUrl"]}/Uploads/Images/{source.ProfilePictureUrl}";
    }
}
using AutoMapper;
using GreenSphere.Application.DTOs.Ratings;
using GreenSphere.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace GreenSphere.Application.Resolvers;

public sealed class UserProfileUrlValueResolver(
    IConfiguration configuration,
    IHttpContextAccessor contextAccessor) : IValueResolver<Rating, RatingDto, string?>
{
    public string? Resolve(Rating source, RatingDto destination, string? destMember, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source.CreatedBy.ProfilePictureUrl))
            return string.Empty;

        return contextAccessor.HttpContext!.Request.IsHttps
            ? $"{configuration["Urls:BaseApiUrl"]}/Uploads/Images/{source.CreatedBy.ProfilePictureUrl}"
            : $"{configuration["Urls:FallbackUrl"]}/Uploads/Images/{source.CreatedBy.ProfilePictureUrl}";
    }
}
using AutoMapper;
using GreenSphere.Application.DTOs.Shorts;
using GreenSphere.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace GreenSphere.Application.Resolvers;

public sealed class ThumbnailUrlValueResolver(
    IConfiguration configuration,
    IHttpContextAccessor contextAccessor) : IValueResolver<Short, ShortDto, string?>
{
    public string? Resolve(Short source, ShortDto destination, string? destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.ThumbnailUrl))
            return contextAccessor.HttpContext!.Request.IsHttps
                ? $"{configuration["Urls:BaseApiUrl"]}/Uploads/Images/{source.ThumbnailUrl}"
                : $"{configuration["Urls:FallbackUrl"]}/Uploads/Images/{source.ThumbnailUrl}";

        return null;
    }
}
using AutoMapper;
using GreenSphere.Application.DTOs.Shorts;
using GreenSphere.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace GreenSphere.Application.Resolvers;

public sealed class ShortUrlValueResolver(
    IConfiguration configuration,
    IHttpContextAccessor contextAccessor) : IValueResolver<Short, ShortDto, string>
{
    public string Resolve(Short source, ShortDto destination, string destMember, ResolutionContext context)
    {
        return contextAccessor.HttpContext!.Request.IsHttps
            ? $"{configuration["Urls:BaseApiUrl"]}/Uploads/Shorts/{source.VideoUrl}"
            : $"{configuration["Urls:FallbackUrl"]}/Uploads/Shorts/{source.VideoUrl}";
    }
}
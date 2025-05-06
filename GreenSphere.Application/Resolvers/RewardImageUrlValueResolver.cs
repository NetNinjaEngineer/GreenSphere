using AutoMapper;
using GreenSphere.Application.DTOs.Rewards;
using GreenSphere.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace GreenSphere.Application.Resolvers;

public sealed class RewardImageUrlValueResolver(
    IConfiguration configuration,
    IHttpContextAccessor contextAccessor) : IValueResolver<Product, RewardDto, string>
{
    public string Resolve(Product source, RewardDto destination, string destMember, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source.Img))
            return string.Empty;

        return contextAccessor.HttpContext!.Request.IsHttps
            ? $"{configuration["Urls:BaseApiUrl"]}/Uploads/Images/{source.Img}"
            : $"{configuration["Urls:FallbackUrl"]}/Uploads/Images/{source.Img}";
    }
}
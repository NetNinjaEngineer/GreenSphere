using AutoMapper;
using GreenSphere.Application.DTOs.Rewards;
using GreenSphere.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace GreenSphere.Application.Resolvers;

public sealed class UserRewardImageUrlValueResolver(
    IConfiguration configuration,
    IHttpContextAccessor contextAccessor) : IValueResolver<UserReward, UserRewardDto, string>
{
    public string Resolve(UserReward source, UserRewardDto destination, string destMember, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source.Product.Img))
            return string.Empty;

        return contextAccessor.HttpContext!.Request.IsHttps
            ? $"{configuration["Urls:BaseApiUrl"]}/Uploads/Images/{source.Product.Img}"
            : $"{configuration["Urls:FallbackUrl"]}/Uploads/Images/{source.Product.Img}";
    }
}
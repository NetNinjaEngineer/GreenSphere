using AutoMapper;
using GreenSphere.Application.DTOs.Products;
using GreenSphere.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace GreenSphere.Application.Resolvers;

public sealed class ProductsImageUrlValueResolver(
    IConfiguration configuration,
    IHttpContextAccessor contextAccessor) : IValueResolver<Product, ProductDto, string>
{
    public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source.Img))
            return string.Empty;

        return contextAccessor.HttpContext!.Request.IsHttps
            ? $"{configuration["Urls:BaseApiUrl"]}/Uploads/Images/{source.Img}"
            : $"{configuration["Urls:FallbackUrl"]}/Uploads/Images/{source.Img}";
    }
}
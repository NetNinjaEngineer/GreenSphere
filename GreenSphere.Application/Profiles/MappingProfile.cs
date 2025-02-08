using AutoMapper;
using GreenSphere.Application.DTOs.Category;
using GreenSphere.Application.DTOs.Products;
using GreenSphere.Application.DTOs.Ratings;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Features.Auth.Commands.Register;
using GreenSphere.Application.Features.Products.Commands.CreateProduct;
using GreenSphere.Application.Resolvers;
using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Entities.Identity;

namespace GreenSphere.Application.Profiles;
public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterCommand, ApplicationUser>();
        CreateMap<ApplicationUser, UserProfileDto>()
            .ForMember(dest => dest.ProfilePictureUrl, options => options.MapFrom<AppUserProfileUrlValueResolver>());

        CreateMap<Rating, RatingDto>()
            .ForMember(dest => dest.CreatedBy,
                options => options.MapFrom(src => string.Concat(src.CreatedBy.FirstName, " ", src.CreatedBy.LastName)))
            .ForMember(dest => dest.ProfilePictureUrl, options => options.MapFrom<UserProfileUrlValueResolver>());

        CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.TotalProducts, options => options.MapFrom(src => src.Products.Count));


        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.ImageUrl, options => options.MapFrom<ProductsImageUrlValueResolver>())
            .ForMember(dest => dest.Price, options => options.MapFrom(src => src.OriginalPrice))
            .ForMember(dest => dest.RecentRatings,
                options => options.MapFrom(src => src.Ratings.OrderByDescending(rating => rating.CreatedAt).Take(5)))
            .ForMember(dest => dest.RatingStatistics, options => options.MapFrom(src => src.Ratings));

        CreateMap<ICollection<Rating>, RatingStatisticsDto>()
            .ForMember(dest => dest.TotalComments,
                options => options.MapFrom(src => src.Count(rating => !string.IsNullOrEmpty(rating.Comment))))
            .ForMember(dest => dest.AverageRating,
                options => options.MapFrom(src => src.Any() ? src.Average(rating => rating.Score) : 0))
            .ForMember(dest => dest.TotalRatings, options => options.MapFrom(src => src.Count))
            .ForMember(dest => dest.FirstRatedDate,
                options => options.MapFrom(src => src.Any() ? src.Min(rating => rating.CreatedAt) : (DateTimeOffset?)null))
            .ForMember(dest => dest.LastRatedDate,
                options => options.MapFrom(src => src.Any() ? src.Max(rating => rating.CreatedAt) : (DateTimeOffset?)null))
            .ForMember(dest => dest.RatingDistribution,
                options => options.MapFrom(src =>
                    src.GroupBy(rating => rating.Score).ToDictionary(selector => selector.Key, g => g.Count())));


        CreateMap<CreateProductCommand, Product>();

    }
}

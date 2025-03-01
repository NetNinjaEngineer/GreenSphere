using AutoMapper;
using GreenSphere.Application.DTOs.Basket;
using GreenSphere.Application.DTOs.Category;
using GreenSphere.Application.DTOs.Order;
using GreenSphere.Application.DTOs.Products;
using GreenSphere.Application.DTOs.Ratings;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Features.Auth.Commands.Register;
using GreenSphere.Application.Features.Categories.Queries.GetCategoryWithProducts;
using GreenSphere.Application.Features.Products.Commands.CreateProduct;
using GreenSphere.Application.Resolvers;
using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Entities.Identity;
using System.Globalization;

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
            .ForMember(dest => dest.TotalProducts, options => options.MapFrom(src => src.Products.Count))
            .ForMember(dest => dest.Name, options => options.MapFrom(src =>
                src.CategoryTranslations.Any(ct => ct.LanguageCode == CultureInfo.CurrentCulture.Name)
                    ? src.CategoryTranslations.FirstOrDefault(pt => pt.LanguageCode == CultureInfo.CurrentCulture.Name)!.Name
                    : src.Name))
            .ForMember(dest => dest.Description, options => options.MapFrom(src =>
                src.CategoryTranslations.Any(ct => ct.LanguageCode == CultureInfo.CurrentCulture.Name)
                    ? src.CategoryTranslations.FirstOrDefault(pt => pt.LanguageCode == CultureInfo.CurrentCulture.Name)!.Description
                    : src.Description));

        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.ImageUrl, options => options.MapFrom<ProductsImageUrlValueResolver>())
            .ForMember(dest => dest.Price, options => options.MapFrom(src => src.OriginalPrice))
            .ForMember(dest => dest.RecentRatings,
                options => options.MapFrom(src => src.Ratings.OrderByDescending(rating => rating.CreatedAt).Take(5)))
            .ForMember(dest => dest.RatingStatistics, options => options.MapFrom(src => src.Ratings))
            .ForMember(dest => dest.Name, options => options.MapFrom(src =>
                src.ProductTranslations.Any(pt => pt.LanguageCode == CultureInfo.CurrentCulture.Name)
                    ? src.ProductTranslations.FirstOrDefault(pt => pt.LanguageCode == CultureInfo.CurrentCulture.Name)!.Name
                    : src.Name))
            .ForMember(dest => dest.Description, options => options.MapFrom(src =>
                src.ProductTranslations.Any(pt => pt.LanguageCode == CultureInfo.CurrentCulture.Name)
                    ? src.ProductTranslations.FirstOrDefault(pt => pt.LanguageCode == CultureInfo.CurrentCulture.Name)!.Description
                    : src.Description));

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

        CreateMap<BasketItem, BasketItemDto>()
            .ForMember(dest => dest.Name, options => options.MapFrom(src =>
                src.Product.ProductTranslations.Any(pt => pt.LanguageCode == CultureInfo.CurrentCulture.Name)
                    ? src.Product.ProductTranslations.FirstOrDefault(
                        pt => pt.LanguageCode == CultureInfo.CurrentCulture.Name)!.Name : src.Product.Name));

        CreateMap<CustomerBasket, BasketDto>()
            .ForMember(dest => dest.OwnerEmail, options => options.MapFrom(src => src.CustomerEmail))
            .ForMember(dest => dest.BasketId, options => options.MapFrom(src => src.Id))
            .ForMember(dest => dest.Items, options => options.MapFrom(src => src.BasketItems));

        CreateMap<Category, CategoryWithProductsDto>()
            .ForMember(dest => dest.Name, options => options.MapFrom(src =>
                src.CategoryTranslations.Any(ct => ct.LanguageCode == CultureInfo.CurrentCulture.Name)
                    ? src.CategoryTranslations.FirstOrDefault(pt => pt.LanguageCode == CultureInfo.CurrentCulture.Name)!.Name
                    : src.Name))
            .ForMember(dest => dest.Description, options => options.MapFrom(src =>
                src.CategoryTranslations.Any(ct => ct.LanguageCode == CultureInfo.CurrentCulture.Name)
                    ? src.CategoryTranslations.FirstOrDefault(pt => pt.LanguageCode == CultureInfo.CurrentCulture.Name)!.Description
                    : src.Description));

        CreateMap<OrderItem, OrderItemDto>();

        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.CreatedBy,
                options => options.MapFrom(src => string.Concat(src.User.FirstName, " ", src.User.LastName)))
            .ForMember(dest => dest.CustomerEmail, options => options.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.OrderItems, options => options.MapFrom(src => src.OrderItems));


    }
}

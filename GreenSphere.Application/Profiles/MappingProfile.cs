using System.Globalization;
using AutoMapper;
using GreenSphere.Application.DTOs.Basket;
using GreenSphere.Application.DTOs.Category;
using GreenSphere.Application.DTOs.Favourite;
using GreenSphere.Application.DTOs.Order;
using GreenSphere.Application.DTOs.Points;
using GreenSphere.Application.DTOs.Products;
using GreenSphere.Application.DTOs.Ratings;
using GreenSphere.Application.DTOs.Shorts;
using GreenSphere.Application.DTOs.Users;
using GreenSphere.Application.Features.Auth.Commands.Register;
using GreenSphere.Application.Features.Categories.Queries.GetCategoryWithProducts;
using GreenSphere.Application.Features.Products.Commands.CreateProduct;
using GreenSphere.Application.Features.Shorts.Commands.CreateShort;
using GreenSphere.Application.Features.Shorts.Commands.CreateShortCategory;
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

        CreateMap<CustomerFavourite, FavouriteDto>()
            .ForMember(dest => dest.FavouriteId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.OwnerEmail, opt => opt.MapFrom(src => src.CustomerEmail))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.FavouriteItems));

        CreateMap<FavouriteItem, FavouriteItemDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                src.Product.ProductTranslations.Any(pt => pt.LanguageCode == CultureInfo.CurrentCulture.Name) ?
                    src.Product.ProductTranslations.FirstOrDefault(
                        pt => pt.LanguageCode == CultureInfo.CurrentCulture.Name)!.Name : src.Product.Name));

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

        CreateMap<Address, AddressDto>();

        CreateMap<FavouriteItemDto, FavouriteItem>();

        CreateMap<FavouriteDto, CustomerFavourite>()
            .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.OwnerEmail))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FavouriteId))
            .ForMember(dest => dest.FavouriteItems, opt => opt.MapFrom(src => src.Items));


        CreateMap<ShortCategory, ShortCategoryDto>()
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => CultureInfo.CurrentCulture.Name == "ar-EG" ? src.NameAr : src.NameEn));

        CreateMap<CreateShortCommand, Short>();

        //CreateMap<Short, Short>()
        //    .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
        //    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTimeOffset.Now));
        //CreateMap<Short, ShortDto>();

        CreateMap<CreateShortCategoryCommand, ShortCategory>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTimeOffset.Now));




        CreateMap<Short, ShortDto>()
            .ForMember(dest => dest.Creator,
                opt => opt.MapFrom(src => string.Concat(src.Creator.FirstName, " ", src.Creator.LastName)))
            .ForMember(dest => dest.VideoUrl, opt => opt.MapFrom<ShortUrlValueResolver>())
            .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom<ThumbnailUrlValueResolver>())
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => CultureInfo.CurrentCulture.Name == "ar-EG" ? src.ShortCategory.NameAr : src.ShortCategory.NameEn));

        CreateMap<UserPoints, PointsDto>();

    }
}

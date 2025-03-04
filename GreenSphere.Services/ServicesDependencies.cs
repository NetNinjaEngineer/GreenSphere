using System.Text;
using GreenSphere.Application.Helpers;
using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Application.Interfaces.Services.Models;
using GreenSphere.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GreenSphere.Services;
public static class ServicesDependencies
{
    public static IServiceCollection AddServicesDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSettings = new JWT();

        configuration.GetSection(nameof(JWT)).Bind(jwtSettings);

        services.AddTransient<IMailService, MailService>();

        services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));

        services.Configure<JWT>(configuration.GetSection(nameof(JWT)));

        services.Configure<JWT>(configuration.GetSection(nameof(JWT)));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddHttpContextAccessor();

        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<IRoleService, RoleService>();

        services.AddScoped<ICurrentUser, CurrentUser>();

        services.AddScoped<IUserService, UserService>();

        services.AddMemoryCache();

        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IProductsService, ProductsService>();

        services.AddScoped<IFileService, FileService>();

        services.AddScoped<IBasketService, BasketService>();

        services.AddScoped<IFavouriteService, FavouriteService>();

        services.AddScoped<IOrderService, OrderService>();

        services.AddScoped<IAddressService, AddressService>();

        return services;
    }
}
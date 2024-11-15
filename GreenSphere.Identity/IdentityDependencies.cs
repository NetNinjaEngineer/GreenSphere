using GreenSphere.Application.Interfaces.Identity;
using GreenSphere.Domain.Identity.Entities;
using GreenSphere.Domain.Identity.Models;
using GreenSphere.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GreenSphere.Identity;

public static class IdentityDependencies
{
    public static IServiceCollection AddIdentityDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationIdentityDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                var duration = Convert.ToDouble(configuration["DefaultLockoutMinutes"]);
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(duration);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
            .AddDefaultTokenProviders();

        services.AddAutoMapper(typeof(IdentityDependencies).Assembly);

        services.AddHttpContextAccessor();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<ICurrentUser, CurrentUser>();

        services.Configure<Jwt>(configuration.GetSection(nameof(Jwt)));

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
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!)),
                ClockSkew = TimeSpan.Zero
            };
        })
        .AddGoogle(options =>
        {
            IConfigurationSection googleKeys = configuration.GetSection("Authentication:Google");
            options.ClientId = googleKeys["ClientId"]!;
            options.ClientSecret = googleKeys["ClientSecret"]!;
        })
        .AddFacebook(options =>
        {
            IConfigurationSection facebookAuthKeys = configuration.GetSection("Authentication:Facebook");
            options.AppId = facebookAuthKeys["AppId"]!;
            options.AppSecret = facebookAuthKeys["AppSecret"]!;
        });

        services.AddScoped<IUserPrivacyService, UserPrivacyService>();

        return services;
    }
}
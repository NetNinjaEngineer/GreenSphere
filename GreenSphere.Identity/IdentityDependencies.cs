using GreenSphere.Application.Interfaces.Identity;
using GreenSphere.Application.Interfaces.Identity.Models;
using GreenSphere.Identity.Entities;
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
        var jwt = new JWT();
        configuration.Bind(jwt);

        services.AddDbContext<ApplicationIdentityDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
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

        services.AddAuthentication(Options =>
        {
            Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(Options =>
            {
                Options.SaveToken = true;
                Options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
                };
            })
            .AddGoogle(options =>
            {
                IConfigurationSection googleKeys = configuration.GetSection("Authentication:Google");
                options.ClientId = googleKeys["ClientId"]!;
                options.ClientSecret = googleKeys["ClientSecret"]!;
            });

        return services;
    }
}

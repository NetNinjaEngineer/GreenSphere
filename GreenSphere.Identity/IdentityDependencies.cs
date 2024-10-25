using GreenSphere.Application.Interfaces.Identity;
using GreenSphere.Identity.Entities;
using GreenSphere.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        return services;
    }
}

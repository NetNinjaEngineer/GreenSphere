using FluentValidation;
using GreenSphere.Application.Authorization.Handlers;
using GreenSphere.Application.Authorization.Requirements;
using GreenSphere.Application.Authorization.Requirements.Models;
using GreenSphere.Application.Filters;
using GreenSphere.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GreenSphere.Application;

public static class ApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddMediatR(options =>
            options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(typeof(ApplicationDependencies).Assembly);
        services.AddSingleton<ApiKeyAuthorizationFilter>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddHttpContextAccessor();

        services.AddAuthorizationBuilder()
            .AddPolicy("CanViewProfile", policy =>
                policy.Requirements.Add(new PrivacyRequirement(Permission.CanViewProfile)));

        services.AddScoped<IAuthorizationHandler, PrivacyAuthorizationHandler>(sp =>
        {
            var service = sp.GetRequiredService<IUserPrivacyService>();
            return new PrivacyAuthorizationHandler(service);
        });

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
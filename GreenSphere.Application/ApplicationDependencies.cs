using GreenSphere.Application.Filters;
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

        return services;
    }
}

using GreenSphere.Application.Interfaces.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace GreenSphere.Infrastructure;

public static class InfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddHttpClient<IFacebookClientApi, FacebookClientApi>(options =>
        {
            options.BaseAddress = new Uri("https://graph.facebook.com/");
            options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        return services;
    }
}

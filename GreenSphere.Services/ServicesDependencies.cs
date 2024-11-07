using GreenSphere.Application.Interfaces.Services;
using GreenSphere.Application.Interfaces.Services.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GreenSphere.Services;
public static class ServicesDependencies
{
    public static IServiceCollection AddServicesDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IMailService, MailService>();

        services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));

        return services;
    }
}

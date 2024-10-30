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
        var sendGridSettings = configuration.GetSection("SendGridSettings");

        services.Configure<SendGridSettings>(options =>
        {
            options.ApiKey = sendGridSettings["ApiKey"]!;
            options.FromEmail = sendGridSettings["FromEmail"]!;
            options.FromName = sendGridSettings["FromName"]!;
        });


        services.AddTransient<IMailService, MailService>();

        services.Configure<MailkitSettings>(configuration.GetSection(nameof(MailkitSettings)));

        return services;
    }
}

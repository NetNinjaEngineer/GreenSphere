using Asp.Versioning;
using GreenSphere.Api.Extensions;
using GreenSphere.Api.Extensions.Swagger;
using GreenSphere.Api.Middlewares;
using GreenSphere.Api.Middlewares.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Localization;
using System.Text.Json.Serialization;

namespace GreenSphere.Api;

public static class ApiDependencies
{
    public static IServiceCollection AddApiDependencies(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
            options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status405MethodNotAllowed));
            options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
            options.OutputFormatters.RemoveType<StringOutputFormatter>();
        })
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddEndpointsApiExplorer();
        services.AddSwaggerDocumentation();

        services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();


        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new QueryStringApiVersionReader("version"),
                new HeaderApiVersionReader("X-version"),
                new MediaTypeApiVersionReader("ver")
            );
        })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });


        services.AddScoped<MigrateDatabaseMiddleware>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                x => x.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

        services.AddGlobalExceptionHandler();

        services.AddLocalization();

        return services;
    }
}
using Asp.Versioning;
using GreenSphere.Api.Extensions;
using GreenSphere.Api.Middlewares;
using GreenSphere.Application;
using GreenSphere.Identity;
using GreenSphere.Infrastructure;
using GreenSphere.Persistence;
using GreenSphere.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status405MethodNotAllowed));
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
    options.OutputFormatters.RemoveType<StringOutputFormatter>();
})
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddInfrastructureDependencies()
    .AddPersistenceDependencies()
    .AddIdentityDependencies(builder.Configuration)
    .AddServicesDependencies(builder.Configuration)
    .AddApplicationDependencies();

builder.Services.AddApiVersioning(options =>
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


builder.Services.AddScoped<MigrateDatabaseMiddleware>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", x =>
        x.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddGlobalExceptionHandler();

var app = builder.Build();

app.UseMiddleware<MigrateDatabaseMiddleware>();

app.UseGlobalExceptionHandler();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.UseSwaggerDocumentation();

app.MapControllers();

app.Run();
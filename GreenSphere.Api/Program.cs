using GreenSphere.Api;
using GreenSphere.Api.Extensions;
using GreenSphere.Api.Extensions.Localization;
using GreenSphere.Api.Extensions.Swagger;
using GreenSphere.Api.Middlewares;
using GreenSphere.Application;
using GreenSphere.Infrastructure;
using GreenSphere.Persistence;
using GreenSphere.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureDependencies()
    .AddPersistenceDependencies(builder.Configuration)
    .AddServicesDependencies(builder.Configuration)
    .AddApplicationDependencies()
    .AddApiDependencies();

var app = builder.Build();

app.UseMiddleware<MigrateDatabaseMiddleware>();

app.UseMiddleware<JwtValidationMiddleware>();

app.UseLocalization();

app.UseGlobalExceptionHandler();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.UseSwaggerDocumentation();

app.MapControllers();

app.Run();
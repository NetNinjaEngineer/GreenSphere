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

//app.UseMiddleware<MigrateDatabaseMiddleware>();

app.UseSwaggerDocumentation();

app.UseMiddleware<JwtValidationMiddleware>();

app.UseGlobalExceptionHandler();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.UseLocalization();

app.MapControllers();

app.Run();
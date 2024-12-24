using GreenSphere.Api;
using GreenSphere.Api.Extensions;
using GreenSphere.Api.Middlewares;
using GreenSphere.Application;
using GreenSphere.Infrastructure;
using GreenSphere.Persistence;
using GreenSphere.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureDependencies()
    .AddPersistenceDependencies(builder.Configuration)
    .AddServicesDependencies(builder.Configuration)
    .AddApplicationDependencies()
    .AddApiDependencies();

var app = builder.Build();

app.UseMiddleware<MigrateDatabaseMiddleware>();

app.UseMiddleware<JwtValidationMiddleware>();

app.UseGlobalExceptionHandler();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.UseSwaggerDocumentation();

app.MapControllers();

app.Run();
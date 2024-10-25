using GreenSphere.Identity;
using Microsoft.EntityFrameworkCore;

namespace GreenSphere.Api.Middlewares;

public class MigrateDatabaseMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var serviceScopeFactory = context.RequestServices.GetRequiredService<IServiceScopeFactory>();
        var serviceScope = serviceScopeFactory.CreateScope();
        var identityDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();
        await identityDbContext.Database.MigrateAsync();
        await next(context);
    }
}

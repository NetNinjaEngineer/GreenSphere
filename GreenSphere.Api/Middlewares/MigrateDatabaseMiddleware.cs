using GreenSphere.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GreenSphere.Api.Middlewares;

public class MigrateDatabaseMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var serviceScopeFactory = context.RequestServices.GetRequiredService<IServiceScopeFactory>();
        var serviceScope = serviceScopeFactory.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
        await dbContext.SeedDatabaseAsync();
        await next(context);
    }
}

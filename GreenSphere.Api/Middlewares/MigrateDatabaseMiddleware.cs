using GreenSphere.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GreenSphere.Api.Middlewares;

public class MigrateDatabaseMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var scopeFactory = context.RequestServices.GetRequiredService<IServiceScopeFactory>();

        var scope = scopeFactory.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.SeedDatabaseAsync();

        if ((await dbContext.Database.GetPendingMigrationsAsync()).Any())
        {
            await dbContext.Database.MigrateAsync();
        }

        if (!await dbContext.Database.CanConnectAsync() ||
            !(await dbContext.Database.GetAppliedMigrationsAsync()).Any())
        {
            await dbContext.Database.MigrateAsync();
        }

        await next(context);
    }
}

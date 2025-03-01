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

        await dbContext.Database.MigrateAsync();

        //await dbContext.SeedDatabaseAsync();

        await next(context);
    }
}

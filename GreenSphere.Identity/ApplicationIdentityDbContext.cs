using GreenSphere.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GreenSphere.Identity;
public class ApplicationIdentityDbContext(
    DbContextOptions<ApplicationIdentityDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationIdentityDbContext).Assembly);
    }
}

using GreenSphere.Domain.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GreenSphere.Identity;
public class ApplicationIdentityDbContext(
    DbContextOptions<ApplicationIdentityDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<PrivacySetting> PrivacySettings { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationIdentityDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.ConfigureWarnings(warnings =>
           warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }
}

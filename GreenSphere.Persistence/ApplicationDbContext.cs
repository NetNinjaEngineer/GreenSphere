﻿using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GreenSphere.Persistence;
public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<ProductTranslation> ProductTranslations { get; set; }
    public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
    public DbSet<FavouriteItem> FavouriteItems { get; set; }
    public DbSet<CustomerBasket> CustomerBaskets { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Short> Shorts { get; set; }
    public DbSet<ShortCategory> ShortCategories { get; set; }
    public DbSet<UserPoints> UserPoints { get; set; }
    public DbSet<UserReward> UserRewards { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.ConfigureWarnings(options =>
            options.Ignore(RelationalEventId.PendingModelChangesWarning));
    }
}

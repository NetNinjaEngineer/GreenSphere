using GreenSphere.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenSphere.Persistence.Configurations;

public sealed class FavouriteItemConfiguration : IEntityTypeConfiguration<FavouriteItem>
{
    public void Configure(EntityTypeBuilder<FavouriteItem> builder)
    {
        builder.HasKey(favouriteItem => favouriteItem.ProductId);

        builder.Property(favouriteItem => favouriteItem.ProductId).ValueGeneratedNever();

        builder.HasOne(favouriteItem => favouriteItem.Product)
            .WithOne()
            .HasForeignKey<FavouriteItem>(favouriteItem => favouriteItem.ProductId)
            .IsRequired();

        builder.Property(favouriteItem => favouriteItem.Price)
            .HasPrecision(18, 2);

        builder.HasOne(favouriteItem => favouriteItem.CustomerFavourite)
            .WithMany(customerFavourite => customerFavourite.FavouriteItems)
            .HasForeignKey(favouriteItem => favouriteItem.CustomerFavouriteId)
            .IsRequired();

        builder.HasIndex(favouriteItem => favouriteItem.ProductId).IsUnique();

        builder.ToTable("FavouriteItems");
    }
}


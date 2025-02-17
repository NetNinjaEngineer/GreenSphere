using GreenSphere.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenSphere.Persistence.Configurations;

public sealed class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.HasKey(basketItem => basketItem.Id);

        builder.Property(basketItem => basketItem.Id).ValueGeneratedNever();

        builder.HasOne(basketItem => basketItem.Product)
            .WithOne()
            .HasForeignKey<BasketItem>(basketItem => basketItem.ProductId)
            .IsRequired();

        builder.Property(basketItem => basketItem.Price)
            .HasPrecision(18, 2);

        builder.HasOne(basketItem => basketItem.CustomerBasket)
            .WithMany(customerBasket => customerBasket.BasketItems)
            .HasForeignKey(basketItem => basketItem.CustomerBasketId)
            .IsRequired();

        builder.HasIndex(basketItem => basketItem.ProductId).IsUnique();

        builder.ToTable("BasketItems");
    }
}
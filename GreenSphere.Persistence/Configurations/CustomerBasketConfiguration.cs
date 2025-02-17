using GreenSphere.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenSphere.Persistence.Configurations;

public sealed class CustomerBasketConfiguration : IEntityTypeConfiguration<CustomerBasket>
{
    public void Configure(EntityTypeBuilder<CustomerBasket> builder)
    {
        builder.HasKey(customerBasket => customerBasket.Id);

        builder.Property(customerBasket => customerBasket.Id).ValueGeneratedNever();

        builder.HasIndex(customerBasket => customerBasket.CustomerEmail).IsUnique();

        builder.ToTable("CustomerBaskets");
    }
}
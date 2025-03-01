using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenSphere.Persistence.Configurations;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(order => order.Id);
        builder.Property(order => order.Id).ValueGeneratedNever();

        builder.Property(order => order.OrderStatus)
            .HasConversion(
                status => status.ToString(),
                status => Enum.Parse<OrderStatus>(status));

        builder.Property(order => order.PaymentMethod)
            .HasConversion(
                paymentMethod => paymentMethod.ToString(),
                paymentMethod => Enum.Parse<PaymentMethod>(paymentMethod));

        builder.HasOne(order => order.User)
            .WithMany(user => user.Orders)
            .HasForeignKey(order => order.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Property(order => order.TotalAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(order => order.DeliveryFee)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(order => order.PaymentStatus)
            .HasConversion(
                paymentStatus => paymentStatus.ToString(),
                paymentStatus => Enum.Parse<PaymentStatus>(paymentStatus));

        builder.ToTable("Orders");
    }
}
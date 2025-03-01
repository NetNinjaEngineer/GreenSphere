using GreenSphere.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenSphere.Persistence.Configurations;

public sealed class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id).ValueGeneratedNever();

        builder.HasOne(a => a.User)
            .WithMany(u => u.Addresses)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.ToTable("Addresses");
    }
}
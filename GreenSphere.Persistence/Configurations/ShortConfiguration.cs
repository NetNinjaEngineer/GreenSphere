using GreenSphere.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenSphere.Persistence.Configurations;

public sealed class ShortConfiguration : IEntityTypeConfiguration<Short>
{
    public void Configure(EntityTypeBuilder<Short> builder)
    {
        builder.HasKey(sc => sc.Id);

        builder.Property(sc => sc.Id).ValueGeneratedNever();

        builder.HasOne(s => s.Creator)
            .WithMany(c => c.Shorts)
            .HasForeignKey(s => s.CreatorId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(s => s.ShortCategory)
            .WithMany(sc => sc.Shorts)
            .HasForeignKey(s => s.ShortCategoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.ToTable("Shorts");
    }
}
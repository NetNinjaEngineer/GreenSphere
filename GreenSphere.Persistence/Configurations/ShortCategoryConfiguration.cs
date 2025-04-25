using GreenSphere.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenSphere.Persistence.Configurations;

public sealed class ShortCategoryConfiguration : IEntityTypeConfiguration<ShortCategory>
{
    public void Configure(EntityTypeBuilder<ShortCategory> builder)
    {
        builder.HasKey(sc => sc.Id);

        builder.Property(sc => sc.Id).ValueGeneratedNever();

        builder.Property(sc => sc.NameEn)
            .HasColumnType("VARCHAR")
            .HasMaxLength(100);

        builder.ToTable("ShortCategories");
    }
}
using GreenSphere.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenSphere.Persistence.Configurations;

internal sealed class CategoryTranslationConfiguration : IEntityTypeConfiguration<CategoryTranslation>
{
    public void Configure(EntityTypeBuilder<CategoryTranslation> builder)
    {
        builder.HasKey(ct => ct.Id);

        builder.Property(ct => ct.Id).ValueGeneratedNever();

        builder.HasOne(ct => ct.Category)
            .WithMany(c => c.CategoryTranslations)
            .HasForeignKey(ct => ct.CategoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.ToTable("CategoryTranslations");
    }
}
using GreenSphere.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenSphere.Persistence.Configurations;
internal sealed class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasKey(rating => rating.Id);

        builder.Property(rating => rating.Id).ValueGeneratedNever();

        builder.Property(r => r.Score)
            .IsRequired()
            .HasAnnotation("Range", (int[])[1, 5]);

        builder.Property(rating => rating.Comment)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.HasOne(rating => rating.CreatedBy)
            .WithMany(user => user.Ratings)
            .HasForeignKey(rating => rating.CreatedById)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(rating => rating.Product)
            .WithMany(product => product.Ratings)
            .HasForeignKey(rating => rating.ProductId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasIndex(rating => rating.ProductId);
        builder.HasIndex(rating => rating.CreatedById);

        builder.HasIndex(rating => new { rating.ProductId, rating.CreatedById }).IsUnique();

        builder.ToTable("Ratings");
    }
}

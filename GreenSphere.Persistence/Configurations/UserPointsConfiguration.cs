using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenSphere.Persistence.Configurations;

public sealed class UserPointsConfiguration : IEntityTypeConfiguration<UserPoints>
{
    public void Configure(EntityTypeBuilder<UserPoints> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).ValueGeneratedNever();

        builder.HasOne(p => p.User)
            .WithMany(u => u.PointsHistory)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Property(p => p.ActivityType)
            .HasConversion(
                type => type.ToString(),
                type => Enum.Parse<ActivityType>(type));

        builder.ToTable("UserPoints");
    }
}
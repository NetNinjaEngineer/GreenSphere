using GreenSphere.Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenSphere.Identity.Configurations;
public sealed class PrivacySettingConfiguration : IEntityTypeConfiguration<PrivacySetting>
{
    public void Configure(EntityTypeBuilder<PrivacySetting> builder)
    {
        builder.ToTable("PrivacySettings");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasOne(x => x.ApplicationUser)
            .WithOne(x => x.PrivacySetting)
            .HasForeignKey<PrivacySetting>(x => x.UserId)
            .IsRequired();
    }
}

using GreenSphere.Domain.Entities;
using GreenSphere.Domain.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenSphere.Persistence.Configurations;
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

        builder.Property(x => x.SendMessages)
            .HasConversion(
                x => x.ToString(),
                x => (MessagePermission)Enum.Parse(typeof(MessagePermission), x)
            );

        builder.Property(x => x.ViewPosts)
           .HasConversion(
               x => x.ToString(),
               x => (PostVisibility)Enum.Parse(typeof(PostVisibility), x)
           );

        builder.Property(x => x.ViewActivityStatus)
         .HasConversion(
             x => x.ToString(),
             x => (ActivityStatusVisibility)Enum.Parse(typeof(ActivityStatusVisibility), x)
         );

        builder.Property(x => x.TagInPosts)
         .HasConversion(
             x => x.ToString(),
             x => (TaggingPermission)Enum.Parse(typeof(TaggingPermission), x)
         );

        builder.Property(x => x.ViewProfile)
         .HasConversion(
             x => x.ToString(),
             x => (ProfileVisibility)Enum.Parse(typeof(ProfileVisibility), x)
         );
    }
}

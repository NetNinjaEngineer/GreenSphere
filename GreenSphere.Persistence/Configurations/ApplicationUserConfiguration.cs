using GreenSphere.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenSphere.Persistence.Configurations;

public sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasData(LoadUsers());
    }

    private static ApplicationUser[] LoadUsers()
    {
        var passwordHasher = new PasswordHasher<ApplicationUser>();

        return
        [
            new ApplicationUser()
            {
                Id = "DB01BCAD-04F0-42E4-9CC1-52C03F3DC635",
                FirstName = "Mohamed",
                LastName = "Ehab",
                UserName = "Moehab@2002",
                Email = "me5260287@gmail.com",
                EmailConfirmed = true,
                NormalizedEmail = "me5260287@gmail.com".ToUpper(),
                NormalizedUserName = "Moehab@2002".ToUpper(),
                PasswordHash = passwordHasher.HashPassword(null, "P@ssw1234")
            }
        ];
    }
}
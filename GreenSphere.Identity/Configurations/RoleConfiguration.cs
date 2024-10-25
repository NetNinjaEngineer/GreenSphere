using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenSphere.Identity.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(LoadRoles());
    }

    private static List<IdentityRole> LoadRoles() =>
    [
        new()
        {
            Id = "D568D764-CEA2-4D01-AAB8-388B6441274C",
            Name = "User",
            NormalizedName = "USER"
        },
        new()
        {
            Id = "9BAF6E8B-D801-4B55-8ABA-4C7A28F7446F",
            Name = "Admin",
            NormalizedName = "ADMIN"
        }
    ];
}

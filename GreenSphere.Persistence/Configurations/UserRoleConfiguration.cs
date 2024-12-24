using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenSphere.Persistence.Configurations;
public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(LoadUserRoles());
    }

    private static IdentityUserRole<string>[] LoadUserRoles()
    {
        return
        [
            new IdentityUserRole<string>()
            {
                UserId = "DB01BCAD-04F0-42E4-9CC1-52C03F3DC635",
                RoleId = "D568D764-CEA2-4D01-AAB8-388B6441274C"
            },
            new IdentityUserRole<string>()
            {
                 UserId = "DB01BCAD-04F0-42E4-9CC1-52C03F3DC635",
                 RoleId = "9BAF6E8B-D801-4B55-8ABA-4C7A28F7446F"
            }
        ];
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenSphere.Persistence.Configurations;
internal sealed class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(LoadUserRoles());
    }

    private static IdentityUserRole<string>[] LoadUserRoles()
    {
        return
        [
            new IdentityUserRole<string>
            {
                UserId = "702C7401-F83C-4684-9421-9AA74FC40050",
                RoleId = "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F"
            },
            new IdentityUserRole<string>
            {
                UserId = "702C7401-F83C-4684-9421-9AA74FC40050",
                RoleId = "BE3B9D48-68F5-42E3-9371-E7964F96A25D"
            },
            new IdentityUserRole<string>
            {
                UserId = "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                RoleId = "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F"
            },
            new IdentityUserRole<string>
            {
                UserId = "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                RoleId = "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F"
            },
            new IdentityUserRole<string>
            {
                UserId = "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                RoleId = "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F"
            },
            new IdentityUserRole<string>
            {
                UserId = "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                RoleId = "BE3B9D48-68F5-42E3-9371-E7964F96A25D"
            },
            new IdentityUserRole<string>
            {
                UserId = "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                RoleId = "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F"
            },
            new IdentityUserRole<string>
            {
                UserId = "B3945AB7-1F46-4829-9DEA-6860E283582F",
                RoleId = "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F"
            },
            new IdentityUserRole<string>
            {
                UserId = "3944C201-0184-4F97-83A6-B6E4852C961F",
                RoleId = "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F"
            },
            new IdentityUserRole<string>
            {
                UserId = "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                RoleId = "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F"
            },
            new IdentityUserRole<string>
            {
                UserId = "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                RoleId = "BE3B9D48-68F5-42E3-9371-E7964F96A25D"
            }
        ];
    }
}

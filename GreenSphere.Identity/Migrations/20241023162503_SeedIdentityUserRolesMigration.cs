using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GreenSphere.Identity.Migrations
{
    /// <inheritdoc />
    public partial class SeedIdentityUserRolesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "9BAF6E8B-D801-4B55-8ABA-4C7A28F7446F", "DB01BCAD-04F0-42E4-9CC1-52C03F3DC635" },
                    { "D568D764-CEA2-4D01-AAB8-388B6441274C", "DB01BCAD-04F0-42E4-9CC1-52C03F3DC635" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "DB01BCAD-04F0-42E4-9CC1-52C03F3DC635",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8290e950-fb1c-4cb2-86a1-e980b0fb37a2", "AQAAAAIAAYagAAAAEMDhAiHydVWBNeXxYpnZSXmWh6C99ucV9DXG8N0m7e6/AB8SnRY0mGq0Ca7JpLzjfA==", "88aa60b0-856f-46e7-8146-83ba7ab12d2e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9BAF6E8B-D801-4B55-8ABA-4C7A28F7446F", "DB01BCAD-04F0-42E4-9CC1-52C03F3DC635" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "D568D764-CEA2-4D01-AAB8-388B6441274C", "DB01BCAD-04F0-42E4-9CC1-52C03F3DC635" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "DB01BCAD-04F0-42E4-9CC1-52C03F3DC635",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "897fa872-87f8-43fb-9cc2-0e716b47fb43", "AQAAAAIAAYagAAAAEDSBV4aR209bbKAHD3qj/5a813ysNGuT49pBmlPcYHtWAn5N90S6WjhuIp3ZqGE04Q==", "85a9a4fa-90bf-421a-8a77-61c15c42d1cf" });
        }
    }
}

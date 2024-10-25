using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenSphere.Identity.Migrations
{
    /// <inheritdoc />
    public partial class SeedAppUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Code", "CodeExpiration", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "DB01BCAD-04F0-42E4-9CC1-52C03F3DC635", 0, null, null, "897fa872-87f8-43fb-9cc2-0e716b47fb43", "me5260287@gmail.com", true, "Mohamed", "Ehab", false, null, "ME5260287@GMAIL.COM", "MOEHAB@2002", "AQAAAAIAAYagAAAAEDSBV4aR209bbKAHD3qj/5a813ysNGuT49pBmlPcYHtWAn5N90S6WjhuIp3ZqGE04Q==", null, false, "85a9a4fa-90bf-421a-8a77-61c15c42d1cf", false, "Moehab@2002" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "DB01BCAD-04F0-42E4-9CC1-52C03F3DC635");
        }
    }
}

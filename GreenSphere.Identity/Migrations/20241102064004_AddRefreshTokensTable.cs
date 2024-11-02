using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenSphere.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokensTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RevokedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => new { x.ApplicationUserId, x.Id });
                    table.ForeignKey(
                        name: "FK_RefreshToken_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "DB01BCAD-04F0-42E4-9CC1-52C03F3DC635",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c0f9579d-fd50-4600-8ef8-df69b8585bab", "AQAAAAIAAYagAAAAEB+Ufa1PCv1/Lsi2B1LlKs0/yZDzMQtwDXjXGLE/BljDEJoqgu+YsLwrxXGVrQjYEw==", "888a3956-d8d2-43fa-a4e2-23eb190672e0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "DB01BCAD-04F0-42E4-9CC1-52C03F3DC635",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8290e950-fb1c-4cb2-86a1-e980b0fb37a2", "AQAAAAIAAYagAAAAEMDhAiHydVWBNeXxYpnZSXmWh6C99ucV9DXG8N0m7e6/AB8SnRY0mGq0Ca7JpLzjfA==", "88aa60b0-856f-46e7-8146-83ba7ab12d2e" });
        }
    }
}

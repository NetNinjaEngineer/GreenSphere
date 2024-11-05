using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenSphere.Identity.Migrations
{
    /// <inheritdoc />
    public partial class CreatePrivacyTableWithOneToOneRelationshipUserPrivacySettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrivacySettings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ViewProfile = table.Column<int>(type: "int", nullable: false),
                    SendMessages = table.Column<int>(type: "int", nullable: false),
                    ViewActivityStatus = table.Column<int>(type: "int", nullable: false),
                    ViewPosts = table.Column<int>(type: "int", nullable: false),
                    TagInPosts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivacySettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivacySettings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "DB01BCAD-04F0-42E4-9CC1-52C03F3DC635",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "75630ac6-47c7-4e89-88aa-24e760e5657d", "AQAAAAIAAYagAAAAEMq6CBr38yX0QyhcwUtRNxQaUmiQkNj+Uay3EBOhtrPXzxkFoRTNnl2FaUe+t9CYww==", "ba63d3cf-3079-4c90-9c0e-0150d0dc0326" });

            migrationBuilder.CreateIndex(
                name: "IX_PrivacySettings_UserId",
                table: "PrivacySettings",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrivacySettings");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "DB01BCAD-04F0-42E4-9CC1-52C03F3DC635",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c0f9579d-fd50-4600-8ef8-df69b8585bab", "AQAAAAIAAYagAAAAEB+Ufa1PCv1/Lsi2B1LlKs0/yZDzMQtwDXjXGLE/BljDEJoqgu+YsLwrxXGVrQjYEw==", "888a3956-d8d2-43fa-a4e2-23eb190672e0" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenSphere.Identity.Migrations
{
    /// <inheritdoc />
    public partial class ConfigPrivacySettingEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ViewProfile",
                table: "PrivacySettings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ViewPosts",
                table: "PrivacySettings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ViewActivityStatus",
                table: "PrivacySettings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TagInPosts",
                table: "PrivacySettings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "SendMessages",
                table: "PrivacySettings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "DB01BCAD-04F0-42E4-9CC1-52C03F3DC635",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8aab10c7-6ab5-43a1-b990-de732969a5ee", "AQAAAAIAAYagAAAAEJ50Icadbz0YZ80PMu38xgpk79UuPgkM7XICUHRfzaWZQF7BAfUg9mTox7SOiznYhQ==", "e90f3439-62d8-4933-ae04-581323a7be24" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ViewProfile",
                table: "PrivacySettings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ViewPosts",
                table: "PrivacySettings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ViewActivityStatus",
                table: "PrivacySettings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "TagInPosts",
                table: "PrivacySettings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "SendMessages",
                table: "PrivacySettings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "DB01BCAD-04F0-42E4-9CC1-52C03F3DC635",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "75630ac6-47c7-4e89-88aa-24e760e5657d", "AQAAAAIAAYagAAAAEMq6CBr38yX0QyhcwUtRNxQaUmiQkNj+Uay3EBOhtrPXzxkFoRTNnl2FaUe+t9CYww==", "ba63d3cf-3079-4c90-9c0e-0150d0dc0326" });
        }
    }
}

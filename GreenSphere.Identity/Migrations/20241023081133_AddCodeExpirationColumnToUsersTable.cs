using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenSphere.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddCodeExpirationColumnToUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CodeExpiration",
                table: "AspNetUsers",
                type: "datetimeoffset",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeExpiration",
                table: "AspNetUsers");
        }
    }
}

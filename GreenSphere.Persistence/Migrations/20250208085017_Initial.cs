using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GreenSphere.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeExpiration = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OriginalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F", null, "User", "USER" },
                    { "BE3B9D48-68F5-42E3-9371-E7964F96A25D", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Code", "CodeExpiration", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "049759F5-3AD8-46BF-89EE-AC51F3BEED88", 0, null, null, "3b717e19-83d7-4167-811b-76339e796681", new DateOnly(1985, 2, 14), "chrisa@example.com", true, "Chris", "Male", "Anderson", false, null, "CHRISA@EXAMPLE.COM", "CHRISA@707", "AQAAAAIAAYagAAAAEEB4FFlq9vu9WYleznKnh5HQKGr2ZodZ5yEcvLVHqarwQU6vevLNa6SfrKq23wcm/w==", null, false, "c8e15d70-078a-4bff-918b-63ee7a199b9f", false, "ChrisA@707" },
                    { "0821819C-64AE-4C73-96F2-4E607AA59D7E", 0, null, null, "b84cf3d0-e5ab-49e2-9106-69588a4c2e6a", new DateOnly(1980, 12, 5), "bobbrown@example.com", true, "Bob", "Male", "Brown", false, null, "BOBBROWN@EXAMPLE.COM", "BOBBROWN@101", "AQAAAAIAAYagAAAAEKhrxcfuWxhDpPbA0//BSvTPZCjLyP7NrlfG6qZ+vNbqSG5AvICK6RHmhRjito/DAQ==", null, false, "435071af-273a-41b1-b85f-f9dd4021cb6a", false, "BobBrown@101" },
                    { "0A9232F3-BC6D-4610-AAFF-F1032831E847", 0, null, null, "6e40e1db-21d9-40ef-9739-2ae88627aec8", new DateOnly(1990, 6, 20), "laurat@example.com", true, "Laura", "Female", "Taylor", false, null, "LAURAT@EXAMPLE.COM", "LAURAT@606", "AQAAAAIAAYagAAAAEM1RuYTT/y/OPd1CaoELkylKPjx+Wc9ELd5jvxttL6XXS2hOy+b8Knrk/r52TAZ5tQ==", null, false, "2c286a3d-8b34-46ad-974d-8e3316da31e7", false, "LauraT@606" },
                    { "3944C201-0184-4F97-83A6-B6E4852C961F", 0, null, null, "0be4267a-6176-4481-8f18-e7837c59f01d", new DateOnly(1975, 11, 12), "davidm@example.com", true, "David", "Male", "Moore", false, null, "DAVIDM@EXAMPLE.COM", "DAVIDM@505", "AQAAAAIAAYagAAAAEE8xTIsZ0o9F/1CphI0TTCby1BFiaXuvI0cMCnpUTq/injJa0ZRRbDYog0DwOGbMXw==", null, false, "ffdcbc31-5497-4fc6-bf8c-33fa3409c2e0", false, "DavidM@505" },
                    { "3EB45CDA-F2EE-43E7-B9F1-D52562E05929", 0, null, null, "fcfff429-b29a-497d-890c-bd32bb332b98", new DateOnly(1990, 5, 15), "johndoe@example.com", true, "John", "Male", "Doe", false, null, "JOHNDOE@EXAMPLE.COM", "JOHNDOE@123", "AQAAAAIAAYagAAAAECvR0C7KPe6e9sITfDnGQ1knD6jJ4AQN139xWIcXyWpzkZzGkiEhnUf8cWt2XNlgfQ==", null, false, "c200f523-6f0b-4c49-b8b8-c060c14a5a41", false, "JohnDoe@123" },
                    { "5326BB55-A26F-47FE-ABC4-9DF44F7B0333", 0, null, null, "582ef659-b1af-474d-8614-d99546845ef0", new DateOnly(1988, 9, 25), "michaelw@example.com", true, "Michael", "Male", "Wilson", false, null, "MICHAELW@EXAMPLE.COM", "MICHAELW@303", "AQAAAAIAAYagAAAAEOLxe/okEFpvwVKH8sqJek/n/Rc5jctg79UqZHBHvQBoGXzFuvMAXdNV9fjkIztk8w==", null, false, "fe616f68-588c-49db-93c6-8bb403269122", false, "MichaelW@303" },
                    { "5B91855C-2D98-4E2B-B919-CDE322C9002D", 0, null, null, "4e699a7c-4c44-4a5a-a00f-6689ba3d830e", new DateOnly(1992, 7, 18), "emilyd@example.com", true, "Emily", "Female", "Davis", false, null, "EMILYD@EXAMPLE.COM", "EMILYD@202", "AQAAAAIAAYagAAAAEMqpC1dLoRjJYy0U5ewSWDNMhZ9RypMScO6/eD7IrAV4HrLdfqeXiaOO6z1SzZpmnA==", null, false, "1b1a46b7-4b51-4538-ae0a-c7fe6c693b83", false, "EmilyD@202" },
                    { "702C7401-F83C-4684-9421-9AA74FC40050", 0, null, null, "cb8b431c-1fb3-410f-9e53-719b3011e003", new DateOnly(2002, 1, 1), "me5260287@gmail.com", true, "Mohamed", "Male", "Ehab", false, null, "ME5260287@GMAIL.COM", "MOEHAB@2002", "AQAAAAIAAYagAAAAEIpvPzyc+4jjcdxIPp6MA2gnr1gohK7Oejy2l1qwowXqfOaaO9gS7HCwh6ve+Qd0ng==", null, false, "20b03ea1-7528-4254-aabb-eb4c219c9fe7", false, "Moehab@2002" },
                    { "9818FAE0-A167-4808-A30D-BC7418A53CB0", 0, null, null, "83882df3-e2c5-4798-ac70-7b70c7fede63", new DateOnly(1985, 8, 22), "janesmith@example.com", true, "Jane", "Female", "Smith", false, null, "JANESMITH@EXAMPLE.COM", "JANESMITH@456", "AQAAAAIAAYagAAAAEAC3JpRPxyLBNkEeeRrzSB8DyJE/3AD8bC1jW+Pz3RRN0GPUmmO+Yi7FfSuyag5sRQ==", null, false, "d65e2c1a-3aeb-4acd-9237-81643f64c8dd", false, "JaneSmith@456" },
                    { "B3945AB7-1F46-4829-9DEA-6860E283582F", 0, null, null, "ea3bb958-6680-4457-9ac3-dfc459d994ef", new DateOnly(1998, 4, 30), "sarahm@example.com", true, "Sarah", "Female", "Miller", false, null, "SARAHM@EXAMPLE.COM", "SARAHM@404", "AQAAAAIAAYagAAAAEFltD+VoxiKkd9yEudWx9uI426L169H7l3u071/v0HNp/K8BadQfAnWF2E5EOHLyyQ==", null, false, "cd8ff0aa-7984-4ee5-880d-efef318277b3", false, "SarahM@404" },
                    { "FE2FB445-6562-49DD-B0A3-77E0A3A1C376", 0, null, null, "cd5cd245-7d50-456f-aca6-0a2437dfdbe9", new DateOnly(1995, 3, 10), "alicej@example.com", true, "Alice", "Female", "Johnson", false, null, "ALICEJ@EXAMPLE.COM", "ALICEJ@789", "AQAAAAIAAYagAAAAEAS5WzGgwbyoLOjuNgKPIwNikX97I7dSg6eMBzbqcV/hdcPbG5bjJD6gXb6wga2EBw==", null, false, "5f59cbc4-5d10-45ec-b1f9-172c6cb71025", false, "AliceJ@789" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "BE3B9D48-68F5-42E3-9371-E7964F96A25D", "049759F5-3AD8-46BF-89EE-AC51F3BEED88" },
                    { "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F", "0A9232F3-BC6D-4610-AAFF-F1032831E847" },
                    { "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F", "3944C201-0184-4F97-83A6-B6E4852C961F" },
                    { "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F", "3EB45CDA-F2EE-43E7-B9F1-D52562E05929" },
                    { "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F", "5326BB55-A26F-47FE-ABC4-9DF44F7B0333" },
                    { "BE3B9D48-68F5-42E3-9371-E7964F96A25D", "5B91855C-2D98-4E2B-B919-CDE322C9002D" },
                    { "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F", "702C7401-F83C-4684-9421-9AA74FC40050" },
                    { "BE3B9D48-68F5-42E3-9371-E7964F96A25D", "702C7401-F83C-4684-9421-9AA74FC40050" },
                    { "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F", "9818FAE0-A167-4808-A30D-BC7418A53CB0" },
                    { "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F", "B3945AB7-1F46-4829-9DEA-6860E283582F" },
                    { "25801C14-CBA0-4E74-8F6A-9AA57BA5A57F", "FE2FB445-6562-49DD-B0A3-77E0A3A1C376" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CreatedById",
                table: "Ratings",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ProductId",
                table: "Ratings",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ProductId_CreatedById",
                table: "Ratings",
                columns: new[] { "ProductId", "CreatedById" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}

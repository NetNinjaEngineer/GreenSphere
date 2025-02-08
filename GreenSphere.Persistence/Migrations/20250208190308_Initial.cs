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
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "CategoryTranslations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryTranslations_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
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
                name: "ProductTranslations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTranslations_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
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
                columns: new[] { "Id", "AccessFailedCount", "Code", "CodeExpiration", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureUrl", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "049759F5-3AD8-46BF-89EE-AC51F3BEED88", 0, null, null, "8bb8de39-1e72-4c55-a784-d16800205b3a", new DateOnly(1985, 2, 14), "chrisa@example.com", true, "Chris", "Male", "Anderson", false, null, "CHRISA@EXAMPLE.COM", "CHRISA@707", "AQAAAAIAAYagAAAAEGEPpq88RJhgdP0ifHma2pvjdzp8gTCqKcuNLDLPcQZOmdokCvL19PhsB3vbZBeUdQ==", null, false, null, "6b6721b6-08d1-4d70-a325-c354bd399555", false, "ChrisA@707" },
                    { "0821819C-64AE-4C73-96F2-4E607AA59D7E", 0, null, null, "139192b4-1239-49c4-924d-ca0f6a602417", new DateOnly(1980, 12, 5), "bobbrown@example.com", true, "Bob", "Male", "Brown", false, null, "BOBBROWN@EXAMPLE.COM", "BOBBROWN@101", "AQAAAAIAAYagAAAAEIrQqOajeDb1Isz/vQpPEMW8sBQGoq8rTFYAn9BNHUGsa95lhBwfHH8fvt/8kNBKPQ==", null, false, null, "f4d1a93c-7060-4487-b1b5-0366727374a1", false, "BobBrown@101" },
                    { "0A9232F3-BC6D-4610-AAFF-F1032831E847", 0, null, null, "c6d08786-e93c-4ebd-9a4c-16ad9832147a", new DateOnly(1990, 6, 20), "laurat@example.com", true, "Laura", "Female", "Taylor", false, null, "LAURAT@EXAMPLE.COM", "LAURAT@606", "AQAAAAIAAYagAAAAEHhOvyPjMkbueU7Np0FdJzM247hsuXlnrJhXKMCO56QMAcN4gYm0MHCj9igqVtJyOQ==", null, false, null, "7a4a6378-7fb5-4938-abac-6df55c37ab6a", false, "LauraT@606" },
                    { "3944C201-0184-4F97-83A6-B6E4852C961F", 0, null, null, "ecd74e5f-555b-4408-9eb9-13b5ba5e67a1", new DateOnly(1975, 11, 12), "davidm@example.com", true, "David", "Male", "Moore", false, null, "DAVIDM@EXAMPLE.COM", "DAVIDM@505", "AQAAAAIAAYagAAAAECfp1BSj1TK5aymPP55NepulXRjk1Sx0dKLsuWKmuhrMFbW/46zKAMA6a9K5QCIFjg==", null, false, null, "43da367b-41b7-4523-97c4-8de77b9d181c", false, "DavidM@505" },
                    { "3EB45CDA-F2EE-43E7-B9F1-D52562E05929", 0, null, null, "5854e60a-602d-499d-87bf-15dccb7c86fd", new DateOnly(1990, 5, 15), "johndoe@example.com", true, "John", "Male", "Doe", false, null, "JOHNDOE@EXAMPLE.COM", "JOHNDOE@123", "AQAAAAIAAYagAAAAEP4t+fmxiiQgB8l4xY6Fao9nk3gqKlT+z36KaI1Gnam8eCGZeQ4FvofheRYFDF3auw==", null, false, null, "10773851-ec83-4ba9-8976-0297f1cb159b", false, "JohnDoe@123" },
                    { "5326BB55-A26F-47FE-ABC4-9DF44F7B0333", 0, null, null, "09aee0b7-35c6-4530-8513-366e21afdb79", new DateOnly(1988, 9, 25), "michaelw@example.com", true, "Michael", "Male", "Wilson", false, null, "MICHAELW@EXAMPLE.COM", "MICHAELW@303", "AQAAAAIAAYagAAAAEPH6Bwzo8KNtnigIrg9OAFsr4d9ldntNL9wQSUuttY6uPfLJb6DV1ka64a+BmjAouA==", null, false, null, "9e8a98f4-28aa-45a8-b1dc-6360e3763009", false, "MichaelW@303" },
                    { "5B91855C-2D98-4E2B-B919-CDE322C9002D", 0, null, null, "f299b7ab-979a-4ecb-bb4e-39a737a79d37", new DateOnly(1992, 7, 18), "emilyd@example.com", true, "Emily", "Female", "Davis", false, null, "EMILYD@EXAMPLE.COM", "EMILYD@202", "AQAAAAIAAYagAAAAEJYf80Rmw3Vjqi2sT36qf0ls75WPNDck8ahQ/3n5mA1vR1oBc7DWVfR4+mSmu4cn6w==", null, false, null, "8863157c-3db6-49c5-9eb7-d80525b1ffa4", false, "EmilyD@202" },
                    { "702C7401-F83C-4684-9421-9AA74FC40050", 0, null, null, "1bda050e-a46a-4a2d-81de-5a597b526d63", new DateOnly(2002, 1, 1), "me5260287@gmail.com", true, "Mohamed", "Male", "Ehab", false, null, "ME5260287@GMAIL.COM", "MOEHAB@2002", "AQAAAAIAAYagAAAAEKEURo+SjclnO0xzHue2mevWKoW7GKC9/daAn5ctDsZpI0/5w37BwfqbTQi8SOkR6Q==", null, false, null, "016be945-7fe2-49b3-ace4-73c82bb386b6", false, "Moehab@2002" },
                    { "9818FAE0-A167-4808-A30D-BC7418A53CB0", 0, null, null, "c3638827-1491-490d-bb6f-a9c3960a17ca", new DateOnly(1985, 8, 22), "janesmith@example.com", true, "Jane", "Female", "Smith", false, null, "JANESMITH@EXAMPLE.COM", "JANESMITH@456", "AQAAAAIAAYagAAAAEIgDXwgPswFwH0ZbO1DYzIGb5aThIvPYAhkWHIcxF5opmwBRuVKQcNBhZLQdamh8xQ==", null, false, null, "1e840e0d-0c38-4583-b81a-fdc90b394171", false, "JaneSmith@456" },
                    { "B3945AB7-1F46-4829-9DEA-6860E283582F", 0, null, null, "fe2870bf-9ff6-40cc-b9b6-c04fd9e099c9", new DateOnly(1998, 4, 30), "sarahm@example.com", true, "Sarah", "Female", "Miller", false, null, "SARAHM@EXAMPLE.COM", "SARAHM@404", "AQAAAAIAAYagAAAAELqDkjzSW8KVtrSkIs2ovQzUsDChKBoXS1CLiygm22QnYrFiWYoFq+0qToIPa8u8kQ==", null, false, null, "ea4fbadd-e1db-4a40-941f-833503342903", false, "SarahM@404" },
                    { "FE2FB445-6562-49DD-B0A3-77E0A3A1C376", 0, null, null, "2b50a670-f90a-4924-8144-3c95e35790e7", new DateOnly(1995, 3, 10), "alicej@example.com", true, "Alice", "Female", "Johnson", false, null, "ALICEJ@EXAMPLE.COM", "ALICEJ@789", "AQAAAAIAAYagAAAAELbNSERzfwLRRWXGd048QaGwgwMZAF4SJbERqVLGhVichRXRURn//3eTvnoSt8jC4w==", null, false, null, "60096633-2ad2-42a8-b8e7-0c127a564848", false, "AliceJ@789" }
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
                name: "IX_CategoryTranslations_CategoryId",
                table: "CategoryTranslations",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTranslations_ProductId",
                table: "ProductTranslations",
                column: "ProductId");

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
                name: "CategoryTranslations");

            migrationBuilder.DropTable(
                name: "ProductTranslations");

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

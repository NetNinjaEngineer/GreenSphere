using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenSphere.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedBasketEntitiesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerBaskets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerBaskets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BasketItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    AddedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CustomerBasketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItems_CustomerBaskets_CustomerBasketId",
                        column: x => x.CustomerBasketId,
                        principalTable: "CustomerBaskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51d69558-e3a6-4bf7-9e42-c135c8fc8ac3", "AQAAAAIAAYagAAAAEGVjTv2Vr37y5QPVHOjb/d70yBHMmRxlf4gDJs8o3GrDtJ3Op85ooTmaQZ6XWqML7A==", "ed4af9f0-7550-4056-b25f-abc6ffb37d24" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e68413f6-921b-4093-ab25-3bdea6ce72e2", "AQAAAAIAAYagAAAAEBaPNUnrIOzre0J3ORZaU0o0Ha55TATtIp0yGtUz4i+nhiHgxTVqjGrEF/H17sZiwA==", "a8fd6267-700b-4272-a1ab-591fc0272d96" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c32a1aee-f9df-4dfa-a510-2f34dfde131d", "AQAAAAIAAYagAAAAEJacv3BTJrJ1qNa+raRhalTNY2x0yMpk7U1i+d/Fws06cl0UuNNjdORrHAqgNbMqPQ==", "069c0a57-55e8-4044-bdae-4a9f3d412533" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c2220b52-851b-46f5-905e-03e4f135fa70", "AQAAAAIAAYagAAAAEDpADM0K+tZy3Qz1IrfN0l7bRFGRc5b1q7U1Vlrx9rHUeVr3kCkMg0yH6R493pF41A==", "3d1723e5-986a-4e40-8153-e44e5ea46b6a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "806534c8-a145-4f8c-90ea-5beec3d344ed", "AQAAAAIAAYagAAAAEKLA7jreJYb/VflbztSsk265jNQyldqdf/siihV2OiEI1mG6uh8xqjrEWJ/RKKBijA==", "92641c51-21aa-44a4-a57e-fcfbdad0de87" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d8031b5d-3a24-4221-8bcc-149be4398951", "AQAAAAIAAYagAAAAELdVAsz8SG1+U/q+B8tVTVnJMisJThNhg1dwHQ8+A6gkWlaHk1EZRixcMLUTDageGA==", "748769d4-26eb-48a2-a242-8654732d7b1c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d55e0159-fff6-4593-8c31-6a6e2fd46efa", "AQAAAAIAAYagAAAAEF+ouqeQgPB8zYxQOYSLUdk2nU93ytt/EJxRefzLbG3ohAmORcxnlURXQegrYnRu9Q==", "53130e68-eaa2-4fc4-9423-f5f4b94a8e91" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "43199f0a-de22-4290-8e51-9d238632d216", "AQAAAAIAAYagAAAAEM3DlYJCAsLO/G+Ad0ML4tMH2RnFYgZHe0e9Q5as00q+3Ihg3O+OcfNAfkN/KZAZwQ==", "5effe3f0-f923-41fd-8222-8048642633a2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51667e90-8471-44c8-93e1-573061abacfc", "AQAAAAIAAYagAAAAENoew8uDCQKoViD7utB22PsL9HkLwQrVFeYPpMhEUf1yDOTq/gMKgNV1JGB4Sa14ng==", "456ee933-bd3a-43ab-81d6-aa6209760040" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e2fd345c-a090-4784-b17e-76334c5505ef", "AQAAAAIAAYagAAAAEEte07c0THntFfYCcM1c4FyYvcKTi2lm+PkXfteQs8lJucdi7CM/vsarf3BldVhLVA==", "22f89dee-4223-4bd7-a414-d179f65178ed" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e2431da-bb93-4721-b044-9d21801c76fa", "AQAAAAIAAYagAAAAEGTOzpMGFQOFKGhQTVJ83JlR6U5x7BVkcfqqgdW/ft87ByH724dqxqIlOtHjNFk67Q==", "7af137b4-2895-4a61-beb7-58b12da876b8" });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_CustomerBasketId",
                table: "BasketItems",
                column: "CustomerBasketId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_ProductId",
                table: "BasketItems",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBaskets_CustomerEmail",
                table: "CustomerBaskets",
                column: "CustomerEmail",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketItems");

            migrationBuilder.DropTable(
                name: "CustomerBaskets");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8bb8de39-1e72-4c55-a784-d16800205b3a", "AQAAAAIAAYagAAAAEGEPpq88RJhgdP0ifHma2pvjdzp8gTCqKcuNLDLPcQZOmdokCvL19PhsB3vbZBeUdQ==", "6b6721b6-08d1-4d70-a325-c354bd399555" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "139192b4-1239-49c4-924d-ca0f6a602417", "AQAAAAIAAYagAAAAEIrQqOajeDb1Isz/vQpPEMW8sBQGoq8rTFYAn9BNHUGsa95lhBwfHH8fvt/8kNBKPQ==", "f4d1a93c-7060-4487-b1b5-0366727374a1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c6d08786-e93c-4ebd-9a4c-16ad9832147a", "AQAAAAIAAYagAAAAEHhOvyPjMkbueU7Np0FdJzM247hsuXlnrJhXKMCO56QMAcN4gYm0MHCj9igqVtJyOQ==", "7a4a6378-7fb5-4938-abac-6df55c37ab6a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ecd74e5f-555b-4408-9eb9-13b5ba5e67a1", "AQAAAAIAAYagAAAAECfp1BSj1TK5aymPP55NepulXRjk1Sx0dKLsuWKmuhrMFbW/46zKAMA6a9K5QCIFjg==", "43da367b-41b7-4523-97c4-8de77b9d181c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5854e60a-602d-499d-87bf-15dccb7c86fd", "AQAAAAIAAYagAAAAEP4t+fmxiiQgB8l4xY6Fao9nk3gqKlT+z36KaI1Gnam8eCGZeQ4FvofheRYFDF3auw==", "10773851-ec83-4ba9-8976-0297f1cb159b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "09aee0b7-35c6-4530-8513-366e21afdb79", "AQAAAAIAAYagAAAAEPH6Bwzo8KNtnigIrg9OAFsr4d9ldntNL9wQSUuttY6uPfLJb6DV1ka64a+BmjAouA==", "9e8a98f4-28aa-45a8-b1dc-6360e3763009" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f299b7ab-979a-4ecb-bb4e-39a737a79d37", "AQAAAAIAAYagAAAAEJYf80Rmw3Vjqi2sT36qf0ls75WPNDck8ahQ/3n5mA1vR1oBc7DWVfR4+mSmu4cn6w==", "8863157c-3db6-49c5-9eb7-d80525b1ffa4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1bda050e-a46a-4a2d-81de-5a597b526d63", "AQAAAAIAAYagAAAAEKEURo+SjclnO0xzHue2mevWKoW7GKC9/daAn5ctDsZpI0/5w37BwfqbTQi8SOkR6Q==", "016be945-7fe2-49b3-ace4-73c82bb386b6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c3638827-1491-490d-bb6f-a9c3960a17ca", "AQAAAAIAAYagAAAAEIgDXwgPswFwH0ZbO1DYzIGb5aThIvPYAhkWHIcxF5opmwBRuVKQcNBhZLQdamh8xQ==", "1e840e0d-0c38-4583-b81a-fdc90b394171" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fe2870bf-9ff6-40cc-b9b6-c04fd9e099c9", "AQAAAAIAAYagAAAAELqDkjzSW8KVtrSkIs2ovQzUsDChKBoXS1CLiygm22QnYrFiWYoFq+0qToIPa8u8kQ==", "ea4fbadd-e1db-4a40-941f-833503342903" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b50a670-f90a-4924-8144-3c95e35790e7", "AQAAAAIAAYagAAAAELbNSERzfwLRRWXGd048QaGwgwMZAF4SJbERqVLGhVichRXRURn//3eTvnoSt8jC4w==", "60096633-2ad2-42a8-b8e7-0c127a564848" });
        }
    }
}

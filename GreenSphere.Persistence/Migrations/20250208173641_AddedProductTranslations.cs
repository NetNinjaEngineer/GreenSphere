using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenSphere.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedProductTranslations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e6d583c9-a0c1-40b8-ba53-a1d8f1ee7189", "AQAAAAIAAYagAAAAEL1Dzb4V8znsd4RWwNDRuj/fKltuGLUwhU9OV9q53TM4JBcHs+yNe0nQPatqUcztaQ==", "a1481e44-f444-4409-a91a-842a5302ec7d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cbaec969-86e3-437c-a472-79568c4a96ad", "AQAAAAIAAYagAAAAEDdq0H/gtOkHIGj5L7CAhj0UDVUCGaeceU++Lqa3Zgr8nyoleQmFvljLus2aPGuSCQ==", "f77ae73e-db2a-46ee-b967-9294cc38be04" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a99d1a86-37ef-48fd-a4ea-df17ef646119", "AQAAAAIAAYagAAAAENChEEIWRWM3mRg4RPmshK/oEXFO+6kBkF1NFiFra/5yHwoIdbmaDD6y3GV0sh2QYg==", "3665da62-394b-483b-84d8-db6e5d4edb5e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5ae172c3-ac98-4226-9f18-fca1742719a0", "AQAAAAIAAYagAAAAEOdTe2YnqfSItulxpBaq598Sj49afkiSsRl9c/YYSP2Y4x513PyJhumbt1JXR1ZxAw==", "f0eb28e1-1d49-493f-8a19-1e9d15ab1b9e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9cef81f4-b398-4a2d-8393-832ee36bd3ae", "AQAAAAIAAYagAAAAEJdeEvPJcLgAJW4u92ws7Xpei5yxnW1+nmkE0bZGwk8WNh76J4Elkkzd3keNj7ZXpg==", "4d5d89b8-938a-43c9-aa27-74cd92e90855" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cdae050b-76f9-4290-b227-a876247b8e08", "AQAAAAIAAYagAAAAEPLTN1a4qk8c+Q/co2arR6ykTWd95mrF0zDD8j9yHWNXTSNnLoLVtt4dq77M3UWEgQ==", "b60ffcdd-8393-47fd-9da0-e417c5c25b8b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f6be611a-7169-44b7-b51f-a1a901197104", "AQAAAAIAAYagAAAAEL2CAbk6+c1MtV/7FMHV0NixLyS5Efk97Ocmf3/WfCNHsyGiN5/Jt7rBa1mUUhJU9A==", "935d776a-a529-43d2-8c95-60ecb0e308ce" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b22f9d87-f68f-4f69-95c5-2e4b84e8d2d2", "AQAAAAIAAYagAAAAEJznZIqYHGoIMa+VwEFDXtsdVEoFrQLcLj4l9gxCtclDyaLtJZY3OtXqObcCzMDjDA==", "3724a24c-a449-4684-8a9e-38a4b00a585b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "92ec597d-0cf8-4cf2-b4b6-fb7814b3136a", "AQAAAAIAAYagAAAAEERhXT6rygGEQr5A8sQ60vekmBlYHLtIW9E2gtZEKljIRfjjkhtsLRih5OwmwLeW/Q==", "55e133d8-00ab-4867-aee8-b7d939b4ae9e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d45d10ef-4bf1-4845-acaf-153b2cdfc79f", "AQAAAAIAAYagAAAAENoBxJZeeSppSvR2uM3w2TraG1IKJ4MZmSrRz4wDTuMsOeKBthnq8F0BMiiD1RAcOQ==", "815e0687-b999-4652-9eed-8c92b7c7d014" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cfd53c27-9962-43e1-bebc-e9fe05543092", "AQAAAAIAAYagAAAAECPIkHg3MIv9duvSna9EH1oBqWSF/PQmxj2UI/DK301kS6gSQbUA0XUg9kr9cS6WTg==", "65c5685f-34f3-46a2-818b-cad7fa20ac81" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTranslations_ProductId",
                table: "ProductTranslations",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductTranslations");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2d988c4f-1490-43e0-94a0-663e5ac12f11", "AQAAAAIAAYagAAAAEDLsZIwTTSe4EkOscImuXtuFSXHYo01BuqYgoILDWkWLWhdSI69r+6fE5+aAPfTIgg==", "bc70e537-6382-4d52-82b9-cd199a2d5cec" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af7bf90f-df02-4c40-94ed-b0f1fd3d0dfa", "AQAAAAIAAYagAAAAEF707Fj1Wrl6z+KAeN14Kxov05BbMWN93kX340wSY7m8g2UTODdyV0jX6WO4j2gGDQ==", "d6072d7d-a66e-49a4-9fe3-cf471933e0d7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aff952f7-4b5b-47de-9a9b-20227c85d079", "AQAAAAIAAYagAAAAEMjRGAKRAddQ/xQ3FnV3J/jnHxqPYQ7u+LcMI/Kz6J8R8o5Q4SDMwtvhGKs669cHLA==", "55006865-b7e7-4e18-a9a5-bd237fd50e88" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "980437a0-3620-413a-bebc-3a4174a2534a", "AQAAAAIAAYagAAAAEJL1BDSD1XsZk1+/ny7Si5zXZGV0X/Cdaqbf0KgZFTgoN+/AFe7ey5iihSYhBcMYVA==", "8d6d91bc-b8e4-4ab3-bf77-4f564f2aea61" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "372aa472-da4f-4dd1-8893-f47b540d764c", "AQAAAAIAAYagAAAAEEuaHIkf3u/1GxLUmQCRQEE7P33fD0gNF5Wg3VEtpLdyauxDPqebwBkDZOfVbHog0w==", "96b326a7-2398-46e5-8c80-599b1844c9f4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ee9b91ea-5b17-4fa5-82e2-944c9b7ae9c0", "AQAAAAIAAYagAAAAEDI8wB2jPWLQ5xNEOSusfg5zfbaoBfvMizsxcPZRyMAUuWS5wDjcwiOXO4HGg5REPQ==", "469426b1-175a-4aaf-8e24-ab2b223c1d13" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0a8fce8b-370c-486f-903f-f22f0ad7eb2d", "AQAAAAIAAYagAAAAEEctZCwETtgi0I3gB/CQYEyhQQB4s9zJ9Ag/WWNYx5U7fK2qFutC91+J8B3NSFYOrA==", "2029ce53-b3b7-4073-aab5-57586765822a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa3d759f-ccc4-4a0e-b665-3cb97d92a7b6", "AQAAAAIAAYagAAAAEOzvujyRjxcVswKwoYxCiuGjWmoL03K6stkL7FE7dPVChCEsUUBxmNU4QXJxRxfTXA==", "67e83bd0-5800-4833-86a1-265146629b7c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89199bf6-208a-4f62-ba54-4f929ce7c6fa", "AQAAAAIAAYagAAAAEEwyyXn8PTv1bP6tvhjjFJ9Vmt3tUw4DyWbgSncftZULxGdoBDCSOhxN+vXf5pISzA==", "b19dbf84-9556-41a2-aadb-f6588db2f3f0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a57db13c-0d68-4d49-90a0-8e9d1d3c1ecb", "AQAAAAIAAYagAAAAEPmvLaoGR47PxeJ7msmvN1iozaIyd//kyXG84x0O+R2pxeBOgu1Gb7PIiOWv00GEyg==", "0203ff0d-ee7c-4605-b6a7-8c0dfc50cb4d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9992ef72-e105-40c0-bfa4-2e9dd13294a5", "AQAAAAIAAYagAAAAECjIEscT32AH19UARY9pm/bzjpfL62DbEZh0FbniB+i7uGSMDYs8xp3ed4AFHgKsuw==", "809ce159-2ea5-4bd0-8c04-b4577ea60325" });
        }
    }
}

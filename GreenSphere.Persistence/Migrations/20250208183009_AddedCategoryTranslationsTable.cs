using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenSphere.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedCategoryTranslationsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a8e3abd0-7e1e-4f51-885e-6a5e6a1a4044", "AQAAAAIAAYagAAAAEKNCWSI+j61SHh62vo5feTLQmZ2oK9Hz4vDG6NeGCNbJ3phZ+EeJEysynjeFgDcZPg==", "b4fdc676-71df-4848-b311-60e24ca800d8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26585792-0abe-41b0-8f96-d87fb6a8e0c7", "AQAAAAIAAYagAAAAEItO60CvQCc+4mQvArILIPvnw2F0tXAZb4W/6H92VFGl0XjKBEGJ3WLunCj6BZWYMQ==", "9876424a-9062-418f-befc-84f34af3fda0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7cd5955d-b5e4-444d-a6f8-fc6b9e5ea61f", "AQAAAAIAAYagAAAAELxmYbjhrGKHPxE9ALNjoEzXyTMXu3MPB3RCn8JU38QQyjBuFYDLl0MRa3ZmxdNKwg==", "c01955aa-749a-4467-a6ba-1c4197676d7b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3946cb96-33f6-42ce-89ce-f3372dd04c40", "AQAAAAIAAYagAAAAEKTT7Y/J0BGv1g9uyomVLDBWSp+OjFdbFXiISbu5pjxwcDqNAIST8Y6zfJwD+t6mrw==", "b8228804-e5ae-4c0c-985d-c61080ca1214" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a08aae0-860c-49a3-9426-4636e85e331c", "AQAAAAIAAYagAAAAEJNZBmrepZ4r5DIH1ZB/xyUigMcZi0gb/vvV6A5hojEOASgSDGPvXdBsZbTExZIFpQ==", "e1d34837-0c66-42a0-8a10-d4646b17ee31" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7e5501cf-ddfa-41e1-9e45-2304b842aaef", "AQAAAAIAAYagAAAAECiWGsM4t8q+Safyq+3Y+foTsJVDmAVsFITWz4wGS61dxfOXskNQFvRLhdhWNMNm7w==", "95a7007f-9687-450e-8ae5-f5c77fa7fc3d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "434793c7-5f04-4fa9-bd4f-965a7559992a", "AQAAAAIAAYagAAAAEOcDZ+QIgJ9elMhqZ3idoXU5Xug4vI3PFhnHlrKxS4Wr7ZmPATHtO2CXvNwXeAZJXA==", "34b52e02-5b42-4ddf-9daa-2eed0a6e01de" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f38cb7ac-2f2a-4a6e-a4d3-78da8170513f", "AQAAAAIAAYagAAAAEFYKeSf3NmI/UbKDfj/E/8zjRmCyE4rq27qrugUNsyc7m1TrSD0SvHnmT7GkeUxNBw==", "3748118d-3dc4-4600-b3c8-61cc83cda0ff" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad6d156c-cdcf-4743-aada-12354f2f8307", "AQAAAAIAAYagAAAAEICf+jtQhMp438dObvAL1uh3Idu45ee+qNIhhuiZAE8a+LuX2xKZj2drtpS/FDVG/g==", "2539b351-0c0e-48e5-8a22-b9e32e270e0e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2ad00550-6f53-45da-aff0-f707ca990aa5", "AQAAAAIAAYagAAAAEIIwm0veVTAHXm3p3GmK+bA6m6Oo05mFVS+UPv98PI9QgC/67NU5WGUZU6Z+/QWTsg==", "274544be-1eb2-4721-b371-f096ccaf579d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "97d742f6-0701-45b4-81b0-cdc2b344408b", "AQAAAAIAAYagAAAAECDmebU4bKMTEthGQ5+h1o5ZUJPHIT8s2e06JKXX2NJq1jZkQ0nyv63P5h7nzlmq1w==", "e228a503-b0af-4e50-b97c-b0e28e769529" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslations_CategoryId",
                table: "CategoryTranslations",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryTranslations");

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
        }
    }
}

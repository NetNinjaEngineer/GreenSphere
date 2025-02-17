using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenSphere.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4cb36378-c34a-425c-8717-27364396a120", "AQAAAAIAAYagAAAAEO3CN21IQmCN+UsyhESfHR62Gn1e88UOv/WSZi+NCV6qGy3bYoxveMrbIunjaM0gMw==", "edbb1a28-d801-42d2-8fa9-616b6060eebb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e68b474a-2903-4ead-93e9-5e410d6208f8", "AQAAAAIAAYagAAAAEHiUi8N+xdtepYFUlMOXwIARYbtwmNJHRWJHKpWLRkKZJ68k+MgUADvyBCVNaEzBhw==", "3b6eaeec-a5c7-42c7-8efa-bd23ca660eba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f9e9088f-972a-4a57-8251-6e4765a8fe27", "AQAAAAIAAYagAAAAEPIboQ8yaHCi5xuHHO3RYQ8v751LpU+N88f2aui2aJ+02Vi2GKL5Z7uE/xQ0XMS/tA==", "183919d0-1790-4ae6-af00-575d0669ddbe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d180cd5c-0116-4a05-9d2b-5c22a88fb1ac", "AQAAAAIAAYagAAAAEC0NlzoGgd3ebfnK8Oa9Pn2Q2xBkz0hBTCPGhP/W20GDxSSxr5SBOZKuAV1+q8twMA==", "939810e9-48be-467b-8ad7-a2c958df74d9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5dc6ba78-166e-4250-909a-9c109b8e05f1", "AQAAAAIAAYagAAAAEAyARVzdVo45FHT2LWoj2058IAp83a6aGw4YJ+o96fpQap2rig9J5Uzy04I0krwDYg==", "239dc4c9-70fd-4e83-97ca-e2211eeeeeaa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eb32091e-2431-42bd-a8fc-7d48dfd8c308", "AQAAAAIAAYagAAAAEMkcWUnN4Y0QnvXU76uEq6WAraQaTgYl699bRrxb+OeMsqdGMz1/Ku4FCE92hxb9YA==", "8277ce64-e82d-43b6-a28d-351dbc796885" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5bf27ea-50b3-4aae-aebe-1a0efa4c9119", "AQAAAAIAAYagAAAAEGsJ5RpTVjVoE8uF6qmVrGed08vUljMm+63bQ/siGyku6wzLXeuYy0p8Uj8PH15wkQ==", "8fe45d63-6220-49d0-abbc-552706b0904b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "565905d9-3bf8-4f68-8f7c-b390d6bac016", "AQAAAAIAAYagAAAAEHtoPc2eJEOjDLhwPEG09+Npql3XtOU3ekUoSX+qhfk0HJqydnc1BfAqko+dMSgwqQ==", "759cdaff-9712-48ad-b443-87786aef9f72" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "933aa097-c3b5-4444-81dd-57c180388482", "AQAAAAIAAYagAAAAEAhEPqwa+JfXx1KRtx7MEYTNu8oMIlYvFHV/L/eyr5UvcENlFr9k7Iuc+++uj430hw==", "69c6e088-c6c0-421e-886d-2ff38f2b904e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1878ad9-a255-4f58-aad6-829fb07a4113", "AQAAAAIAAYagAAAAECdkkSnfijquxbiigF4wEIAdYrIUYRISZ8scrTazkMOJPeP3JpfgCwlB2t/SC3M14A==", "054f22e5-344d-4ea5-93b7-7135fd5e3e5a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "39afee9c-f32a-454a-8342-3adc15510f2e", "AQAAAAIAAYagAAAAEAJWRbQZkgc45iYmX1G1pjb8RyD7NADfDPlUcbT3Gt00E85/qK6YJxcJRGlcW93KrQ==", "faf16b92-782f-427d-aaf1-0c9a83c53f25" });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

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

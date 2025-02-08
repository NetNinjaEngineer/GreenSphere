using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenSphere.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedProfilePictureUrlToApplicationUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "80157abc-e273-41e2-a572-181d2b932f2a", "AQAAAAIAAYagAAAAEGc764asluIOepQxbFAhoFY6NFXMioVtEZuD0pp4IZlfLA7Ew7FcBkA8ExFSPOVVyQ==", null, "180fb0f6-1ebc-4e03-82f4-cf10184b8128" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "96373abb-ad0d-45ff-a6d5-9a16d249072f", "AQAAAAIAAYagAAAAELqT+fqfDMRGefrE+rXPtIeS6polzfZsccu/wvk4uMLqbbB1K5kF4mIHXNPQBaZBtQ==", null, "13141ad8-0c9a-4ce7-90b3-35fdd3f38166" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "dffbff31-6a0f-49b0-9299-c4c6cb02512f", "AQAAAAIAAYagAAAAEO4Wvrn0U7G2TZ722vVwg/a+umFGhxKSWaI6hYzoY81hOcEGBNfd7IQ1mnpwpEAK3g==", null, "44fcfd7b-b686-4cf1-ac60-6baacd6f7ce8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "43a4b33d-8288-4aff-afee-05b41e77d2f4", "AQAAAAIAAYagAAAAEJjSffDSMZQH1p/+Rh/ofUTkJ0Iwcu4MyJ+yqfIM/C8W5voRe9fyOnorEbOjmqm3eg==", null, "6d38c299-fc3e-4a23-9c47-167d5a7b8383" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "3fdc508b-f05d-4d76-b84e-065b2d7b17ca", "AQAAAAIAAYagAAAAEGcURfoS1Z+ZheBI2jlAWS0RVyqJfIgcO2ra9f0oPBYEDJUsFDVdGGF3qsdb1Gu4Ew==", null, "7e810ea9-24e2-43af-af3f-07bad6aa9cb6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "313ee7bf-1ed4-4337-9d2b-707a31d769cd", "AQAAAAIAAYagAAAAEI1YuR7o1OUwblNwQkEzlwDu/Mslsq/V9xswAgAMrjhERj4erajJDR3GVLIoFeHZjA==", null, "ccea63de-953c-4301-aecd-c91f93e56fed" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "e08b1820-f844-433f-bec9-b0af63f7dcc4", "AQAAAAIAAYagAAAAEFDmGnKJTBdXZg64alTuc5HAA0aZOTZRzPz0RqV5S7z87IbmORkQUf4Y5trSCMT/Mw==", null, "900aea8d-7646-4753-a9a8-4dc3b6b95045" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "ea9a043b-e2bc-46f0-a619-60f9b1439861", "AQAAAAIAAYagAAAAEMkDt8XKxygDVSY6hgZqdocj1uRQzgUmZ1GcWYZ6PeV/Jzf6YDQWuWa8L0GNtbp3Tg==", null, "af6303da-7bdb-4340-9994-6e7bf150ca03" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "badca42d-4c21-43dd-ae9d-bea2fb8c5b5f", "AQAAAAIAAYagAAAAELKAnilcTvNEB3tow3KYMp++A49OV5nt9+agGTvAELqp3dJ2d2OA1fDKILU0AQEU2Q==", null, "0db04f86-a9b2-4d92-8b10-eb041a67d701" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "451ecdef-9d3e-49c5-999f-3edd1303e5a6", "AQAAAAIAAYagAAAAEEeRwT/JkDs1Ce/1yDSibpXV629L2Vta75/GrDCTof2U1y7cBdeyjRBJipeDeJ+KXQ==", null, "ad10288b-3e93-4edd-830e-6fbcd7817284" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePictureUrl", "SecurityStamp" },
                values: new object[] { "4b8ee2d7-fabd-4d33-9610-8c8f268edd4b", "AQAAAAIAAYagAAAAECiAYpQG2EUrlDV882QtbcHTd/VVuiaxYatfeas7gmL6O6mT352DDDXxcMMEV/X2dA==", null, "92e596da-d146-44b5-ae38-f880394b13b6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b717e19-83d7-4167-811b-76339e796681", "AQAAAAIAAYagAAAAEEB4FFlq9vu9WYleznKnh5HQKGr2ZodZ5yEcvLVHqarwQU6vevLNa6SfrKq23wcm/w==", "c8e15d70-078a-4bff-918b-63ee7a199b9f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b84cf3d0-e5ab-49e2-9106-69588a4c2e6a", "AQAAAAIAAYagAAAAEKhrxcfuWxhDpPbA0//BSvTPZCjLyP7NrlfG6qZ+vNbqSG5AvICK6RHmhRjito/DAQ==", "435071af-273a-41b1-b85f-f9dd4021cb6a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e40e1db-21d9-40ef-9739-2ae88627aec8", "AQAAAAIAAYagAAAAEM1RuYTT/y/OPd1CaoELkylKPjx+Wc9ELd5jvxttL6XXS2hOy+b8Knrk/r52TAZ5tQ==", "2c286a3d-8b34-46ad-974d-8e3316da31e7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3944C201-0184-4F97-83A6-B6E4852C961F",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0be4267a-6176-4481-8f18-e7837c59f01d", "AQAAAAIAAYagAAAAEE8xTIsZ0o9F/1CphI0TTCby1BFiaXuvI0cMCnpUTq/injJa0ZRRbDYog0DwOGbMXw==", "ffdcbc31-5497-4fc6-bf8c-33fa3409c2e0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fcfff429-b29a-497d-890c-bd32bb332b98", "AQAAAAIAAYagAAAAECvR0C7KPe6e9sITfDnGQ1knD6jJ4AQN139xWIcXyWpzkZzGkiEhnUf8cWt2XNlgfQ==", "c200f523-6f0b-4c49-b8b8-c060c14a5a41" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "582ef659-b1af-474d-8614-d99546845ef0", "AQAAAAIAAYagAAAAEOLxe/okEFpvwVKH8sqJek/n/Rc5jctg79UqZHBHvQBoGXzFuvMAXdNV9fjkIztk8w==", "fe616f68-588c-49db-93c6-8bb403269122" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4e699a7c-4c44-4a5a-a00f-6689ba3d830e", "AQAAAAIAAYagAAAAEMqpC1dLoRjJYy0U5ewSWDNMhZ9RypMScO6/eD7IrAV4HrLdfqeXiaOO6z1SzZpmnA==", "1b1a46b7-4b51-4538-ae0a-c7fe6c693b83" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "702C7401-F83C-4684-9421-9AA74FC40050",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cb8b431c-1fb3-410f-9e53-719b3011e003", "AQAAAAIAAYagAAAAEIpvPzyc+4jjcdxIPp6MA2gnr1gohK7Oejy2l1qwowXqfOaaO9gS7HCwh6ve+Qd0ng==", "20b03ea1-7528-4254-aabb-eb4c219c9fe7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83882df3-e2c5-4798-ac70-7b70c7fede63", "AQAAAAIAAYagAAAAEAC3JpRPxyLBNkEeeRrzSB8DyJE/3AD8bC1jW+Pz3RRN0GPUmmO+Yi7FfSuyag5sRQ==", "d65e2c1a-3aeb-4acd-9237-81643f64c8dd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B3945AB7-1F46-4829-9DEA-6860E283582F",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea3bb958-6680-4457-9ac3-dfc459d994ef", "AQAAAAIAAYagAAAAEFltD+VoxiKkd9yEudWx9uI426L169H7l3u071/v0HNp/K8BadQfAnWF2E5EOHLyyQ==", "cd8ff0aa-7984-4ee5-880d-efef318277b3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cd5cd245-7d50-456f-aca6-0a2437dfdbe9", "AQAAAAIAAYagAAAAEAS5WzGgwbyoLOjuNgKPIwNikX97I7dSg6eMBzbqcV/hdcPbG5bjJD6gXb6wga2EBw==", "5f59cbc4-5d10-45ec-b1f9-172c6cb71025" });
        }
    }
}

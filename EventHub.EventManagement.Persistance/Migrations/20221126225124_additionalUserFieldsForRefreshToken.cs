using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHub.EventManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class additionalUserFieldsForRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26d0a47f-367a-4213-ba9f-0b07629e1f85");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69c0b883-976e-4436-a90d-c8d5022b9c03");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "815f5a23-70ad-445e-a4e1-dadf3970a2e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ed65967-29e8-4514-b582-5b8597ce7895");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4ba411aa-438d-4e83-aa69-d025944f26a3", "c2ad2e71-ba69-40bf-b87f-9d883ecaa8da", "Manager", "MANAGER" },
                    { "8bf2b1ec-6087-405d-b948-9a851e410d6e", "5e3fa0ce-f4bd-4268-9bb6-35a2e099cc99", "Producer", "PRODUCER" },
                    { "d50bc62e-2168-44f3-ae24-4512f4be6351", "9ff59123-6c96-4702-9de9-923679d936ba", "Organization", "ORGANIZATION" },
                    { "dc207af5-b9e9-4452-85f8-29a6091c3b81", "1d2cc39b-87fb-4e9a-a556-d85e1248970e", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ba411aa-438d-4e83-aa69-d025944f26a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8bf2b1ec-6087-405d-b948-9a851e410d6e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d50bc62e-2168-44f3-ae24-4512f4be6351");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc207af5-b9e9-4452-85f8-29a6091c3b81");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "26d0a47f-367a-4213-ba9f-0b07629e1f85", "cc02892d-d429-4418-8cdb-6fd2a5c708b7", "Organization", "ORGANIZATION" },
                    { "69c0b883-976e-4436-a90d-c8d5022b9c03", "610b0d12-fce2-4386-9fb4-0e93194ccc15", "Manager", "MANAGER" },
                    { "815f5a23-70ad-445e-a4e1-dadf3970a2e0", "bc2a688c-6ebe-4d3d-bc25-664e86568acb", "Producer", "PRODUCER" },
                    { "9ed65967-29e8-4514-b582-5b8597ce7895", "06137f7a-e88a-4bc8-85ac-cf08ea9b52de", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}

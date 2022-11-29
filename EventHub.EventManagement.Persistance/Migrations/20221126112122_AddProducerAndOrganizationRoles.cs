using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHub.EventManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddProducerAndOrganizationRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "081f8a93-0f24-46c2-9a73-dc9b5ee58bb3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e60a1570-c65d-4682-b8f1-dbe15b81f378");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "081f8a93-0f24-46c2-9a73-dc9b5ee58bb3", "6acd1e2d-27c9-46a6-9b2f-0cd99ea10454", "Administrator", "ADMINISTRATOR" },
                    { "e60a1570-c65d-4682-b8f1-dbe15b81f378", "c84ba809-842d-460b-b148-0ee789e37c73", "Manager", "MANAGER" }
                });
        }
    }
}

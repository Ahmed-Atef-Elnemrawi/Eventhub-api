using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHub.EventManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class RenameuserIdToUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39e58e28-c5a9-4e91-9f99-8feb0b624864");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69e4826e-40de-46b7-9e84-a701869e5ea3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8580204c-8986-4ae4-837e-f7668b8259f4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8aeb7d0a-0b5f-4327-826c-cffb702ce51c");

            migrationBuilder.RenameColumn(
                name: "serId",
                table: "Producers",
                newName: "UserId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1aae6e02-269f-44f3-a59f-f1c8465ba621", null, "Manager", "MANAGER" },
                    { "214cbc80-7bd3-4940-8c3c-6b37705074ca", null, "Producer", "PRODUCER" },
                    { "bf04f7c3-c7b5-4aed-b462-72a61e4d4212", null, "Organization", "ORGANIZATION" },
                    { "d81b7caa-90e3-48e0-b4f7-f19407ee7616", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1aae6e02-269f-44f3-a59f-f1c8465ba621");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "214cbc80-7bd3-4940-8c3c-6b37705074ca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf04f7c3-c7b5-4aed-b462-72a61e4d4212");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d81b7caa-90e3-48e0-b4f7-f19407ee7616");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Producers",
                newName: "serId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "39e58e28-c5a9-4e91-9f99-8feb0b624864", null, "Administrator", "ADMINISTRATOR" },
                    { "69e4826e-40de-46b7-9e84-a701869e5ea3", null, "Organization", "ORGANIZATION" },
                    { "8580204c-8986-4ae4-837e-f7668b8259f4", null, "Producer", "PRODUCER" },
                    { "8aeb7d0a-0b5f-4327-826c-cffb702ce51c", null, "Manager", "MANAGER" }
                });
        }
    }
}

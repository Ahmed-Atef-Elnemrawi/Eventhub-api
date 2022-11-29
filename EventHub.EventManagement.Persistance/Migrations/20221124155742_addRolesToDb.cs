using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHub.EventManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "081f8a93-0f24-46c2-9a73-dc9b5ee58bb3", "6acd1e2d-27c9-46a6-9b2f-0cd99ea10454", "Administrator", "ADMINISTRATOR" },
                    { "e60a1570-c65d-4682-b8f1-dbe15b81f378", "c84ba809-842d-460b-b148-0ee789e37c73", "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "081f8a93-0f24-46c2-9a73-dc9b5ee58bb3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e60a1570-c65d-4682-b8f1-dbe15b81f378");
        }
    }
}

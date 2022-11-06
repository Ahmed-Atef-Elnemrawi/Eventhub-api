using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventHub.EventManagement.Persistance.Migrations
{
    public partial class InsertMediumData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Mediums",
                columns: new[] { "MediumId", "Type" },
                values: new object[,]
                {
                    { new Guid("07ba7806-dad3-447f-8233-564fbc6c1010"), 0 },
                    { new Guid("581b8af3-81eb-4d29-865c-2686b6a0ce9b"), 1 },
                    { new Guid("6691efdb-7f06-4da1-a31b-c29912c49ac4"), 3 },
                    { new Guid("ff1b224d-a32e-473b-9684-df0493140094"), 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Mediums",
                keyColumn: "MediumId",
                keyValue: new Guid("07ba7806-dad3-447f-8233-564fbc6c1010"));

            migrationBuilder.DeleteData(
                table: "Mediums",
                keyColumn: "MediumId",
                keyValue: new Guid("581b8af3-81eb-4d29-865c-2686b6a0ce9b"));

            migrationBuilder.DeleteData(
                table: "Mediums",
                keyColumn: "MediumId",
                keyValue: new Guid("6691efdb-7f06-4da1-a31b-c29912c49ac4"));

            migrationBuilder.DeleteData(
                table: "Mediums",
                keyColumn: "MediumId",
                keyValue: new Guid("ff1b224d-a32e-473b-9684-df0493140094"));
        }
    }
}

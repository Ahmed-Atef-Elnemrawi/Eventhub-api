using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHub.EventManagement.Persistance.Migrations
{
   /// <inheritdoc />
   public partial class adduserIdidentifiercolumntoorganizationTable : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {

         migrationBuilder.AddColumn<Guid>(
             name: "UserId",
             table: "Organizations",
             type: "uniqueidentifier",
             nullable: false,
             defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {


         migrationBuilder.DropColumn(
             name: "UserId",
             table: "Organizations");

      }
   }
}

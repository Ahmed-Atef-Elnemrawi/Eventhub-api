using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHub.EventManagement.Persistance.Migrations
{
   /// <inheritdoc />
   public partial class AddCreatedDateandLastModifiedDatecolumns : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {

         migrationBuilder.AddColumn<DateTime>(
             name: "CreatedDate",
             table: "Producers",
             type: "datetime2",
             nullable: false,
             defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

         migrationBuilder.AddColumn<DateTime>(
             name: "LastModifiedDate",
             table: "Producers",
             type: "datetime2",
             nullable: false,
             defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));



      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {


         migrationBuilder.DropColumn(
             name: "CreatedDate",
             table: "Producers");


         migrationBuilder.DropColumn(
             name: "LastModifiedDate",
             table: "Producers");


      }
   }
}

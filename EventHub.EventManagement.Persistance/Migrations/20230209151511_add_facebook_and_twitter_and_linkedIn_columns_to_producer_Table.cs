using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHub.EventManagement.Persistance.Migrations
{
   /// <inheritdoc />
   public partial class addfacebookandtwitterandlinkedIncolumnstoproducerTable : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {


         migrationBuilder.AddColumn<string>(
             name: "Facebook",
             table: "Producers",
             type: "nvarchar(max)",
             nullable: true);

         migrationBuilder.AddColumn<string>(
             name: "LinkedIn",
             table: "Producers",
             type: "nvarchar(max)",
             nullable: true);

         migrationBuilder.AddColumn<string>(
             name: "Twitter",
             table: "Producers",
             type: "nvarchar(max)",
             nullable: true);


      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {


         migrationBuilder.DropColumn(
             name: "Facebook",
             table: "Producers");

         migrationBuilder.DropColumn(
             name: "LinkedIn",
             table: "Producers");

         migrationBuilder.DropColumn(
             name: "Twitter",
             table: "Producers");


      }
   }
}

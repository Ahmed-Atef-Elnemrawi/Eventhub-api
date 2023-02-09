using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHub.EventManagement.Persistance.Migrations
{
   /// <inheritdoc />
   public partial class changeUserPageidcolumnNameToUserPageId : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {


         migrationBuilder.RenameColumn(
             name: "Id",
             table: "UserPage",
             newName: "UserPageId");


      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {

         migrationBuilder.RenameColumn(
             name: "UserPageId",
             table: "UserPage",
             newName: "Id");


      }
   }
}

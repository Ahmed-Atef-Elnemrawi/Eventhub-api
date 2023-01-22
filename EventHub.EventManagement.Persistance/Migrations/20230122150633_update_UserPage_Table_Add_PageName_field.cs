using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHub.EventManagement.Persistance.Migrations
{
   /// <inheritdoc />
   public partial class updateUserPageTableAddPageNamefield : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {

         migrationBuilder.AddColumn<string>(
             name: "PageName",
             table: "UsersPages",
             type: "nvarchar(max)",
             nullable: true);

      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {


         migrationBuilder.DropColumn(
             name: "PageName",
             table: "UsersPages");


      }
   }
}

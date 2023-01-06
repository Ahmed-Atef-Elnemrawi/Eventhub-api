using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHub.EventManagement.Persistance.Migrations
{
   /// <inheritdoc />
   public partial class AddProfilePictureColumnToUserTable : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {

         migrationBuilder.AddColumn<byte[]>(
             name: "ProfilePicture",
             table: "AspNetUsers",
             type: "varbinary(max)",
             nullable: true);

      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {


         migrationBuilder.DropColumn(
             name: "ProfilePicture",
             table: "AspNetUsers");


      }
   }
}

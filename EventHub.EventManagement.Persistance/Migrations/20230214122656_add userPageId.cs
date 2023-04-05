using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHub.EventManagement.Persistance.Migrations
{
   /// <inheritdoc />
   public partial class adduserPageId : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropForeignKey(
             name: "FK_AspNetUsers_UserPage_UserPageId",
             table: "AspNetUsers");


         migrationBuilder.RenameColumn(
             name: "Id",
             table: "UserPage",
             newName: "UserPageId");



         migrationBuilder.AddForeignKey(
             name: "FK_AspNetUsers_UserPage_UserPageId",
             table: "AspNetUsers",
             column: "UserPageId",
             principalTable: "UserPage",
             principalColumn: "UserPageId",
             onDelete: ReferentialAction.Cascade);
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropForeignKey(
             name: "FK_AspNetUsers_UserPage_UserPageId",
             table: "AspNetUsers");

         migrationBuilder.RenameColumn(
             name: "UserPageId",
             table: "UserPage",
             newName: "Id");




         migrationBuilder.AddForeignKey(
             name: "FK_AspNetUsers_UserPage_UserPageId",
             table: "AspNetUsers",
             column: "UserPageId",
             principalTable: "UserPage",
             principalColumn: "Id");
      }
   }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHub.EventManagement.Persistance.Migrations
{
   /// <inheritdoc />
   public partial class alteruserPageIdtobenullable : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropForeignKey(
             name: "FK_AspNetUsers_UserPage_UserPageId",
             table: "AspNetUsers");



         migrationBuilder.AlterColumn<Guid>(
             name: "UserPageId",
             table: "AspNetUsers",
             type: "uniqueidentifier",
             nullable: true,
             oldClrType: typeof(Guid),
             oldType: "uniqueidentifier");



         migrationBuilder.AddForeignKey(
             name: "FK_AspNetUsers_UserPage_UserPageId",
             table: "AspNetUsers",
             column: "UserPageId",
             principalTable: "UserPage",
             principalColumn: "UserPageId");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropForeignKey(
             name: "FK_AspNetUsers_UserPage_UserPageId",
             table: "AspNetUsers");



         migrationBuilder.AlterColumn<Guid>(
             name: "UserPageId",
             table: "AspNetUsers",
             type: "uniqueidentifier",
             nullable: false,
             defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
             oldClrType: typeof(Guid),
             oldType: "uniqueidentifier",
             oldNullable: true);



         migrationBuilder.AddForeignKey(
             name: "FK_AspNetUsers_UserPage_UserPageId",
             table: "AspNetUsers",
             column: "UserPageId",
             principalTable: "UserPage",
             principalColumn: "UserPageId",
             onDelete: ReferentialAction.Cascade);
      }
   }
}

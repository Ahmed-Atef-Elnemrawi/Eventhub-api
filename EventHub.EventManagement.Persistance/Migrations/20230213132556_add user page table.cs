using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHub.EventManagement.Persistance.Migrations
{
   /// <inheritdoc />
   public partial class adduserpagetable : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {


         migrationBuilder.AddColumn<Guid>(
             name: "UserPageId",
             table: "AspNetUsers",
             type: "uniqueidentifier",
             nullable: true);

         migrationBuilder.CreateTable(
             name: "UserPage",
             columns: table => new
             {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Type = table.Column<int>(type: "int", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_UserPage", x => x.Id);
             });


         migrationBuilder.CreateIndex(
             name: "IX_AspNetUsers_UserPageId",
             table: "AspNetUsers",
             column: "UserPageId");

         migrationBuilder.AddForeignKey(
             name: "FK_AspNetUsers_UserPage_UserPageId",
             table: "AspNetUsers",
             column: "UserPageId",
             principalTable: "UserPage",
             principalColumn: "Id");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropForeignKey(
             name: "FK_AspNetUsers_UserPage_UserPageId",
             table: "AspNetUsers");

         migrationBuilder.DropTable(
             name: "UserPage");

         migrationBuilder.DropIndex(
             name: "IX_AspNetUsers_UserPageId",
             table: "AspNetUsers");



         migrationBuilder.DropColumn(
             name: "UserPageId",
             table: "AspNetUsers");


      }
   }
}

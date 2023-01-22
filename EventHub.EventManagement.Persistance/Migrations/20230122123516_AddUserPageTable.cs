using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHub.EventManagement.Persistance.Migrations
{
   /// <inheritdoc />
   public partial class AddUserPageTable : Migration
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
                UserPageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ProducerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_UserPage", x => x.UserPageId);
                table.ForeignKey(
                       name: "FK_UserPage_Organizations_OrganizationId",
                       column: x => x.OrganizationId,
                       principalTable: "Organizations",
                       principalColumn: "OrganizationId");
                table.ForeignKey(
                       name: "FK_UserPage_Producers_ProducerId",
                       column: x => x.ProducerId,
                       principalTable: "Producers",
                       principalColumn: "ProducerId");
             });



         migrationBuilder.CreateIndex(
             name: "IX_AspNetUsers_UserPageId",
             table: "AspNetUsers",
             column: "UserPageId");

         migrationBuilder.CreateIndex(
             name: "IX_UserPage_OrganizationId",
             table: "UserPage",
             column: "OrganizationId");

         migrationBuilder.CreateIndex(
             name: "IX_UserPage_ProducerId",
             table: "UserPage",
             column: "ProducerId");

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

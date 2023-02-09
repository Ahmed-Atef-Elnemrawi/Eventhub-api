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
             nullable: false,
             defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

         migrationBuilder.CreateTable(
             name: "UserPage",
             columns: table => new
             {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ProducerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_UserPage", x => x.Id);
                table.ForeignKey(
                       name: "FK_UserPage_AspNetUsers_UserId",
                       column: x => x.UserId,
                       principalTable: "AspNetUsers",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
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
             name: "IX_UserPage_OrganizationId",
             table: "UserPage",
             column: "OrganizationId");

         migrationBuilder.CreateIndex(
             name: "IX_UserPage_ProducerId",
             table: "UserPage",
             column: "ProducerId");

         migrationBuilder.CreateIndex(
             name: "IX_UserPage_UserId",
             table: "UserPage",
             column: "UserId",
             unique: true);
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropTable(
             name: "UserPage");



         migrationBuilder.DropColumn(
             name: "UserPageId",
             table: "AspNetUsers");


      }
   }
}

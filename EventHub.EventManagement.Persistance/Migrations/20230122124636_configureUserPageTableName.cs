using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHub.EventManagement.Persistance.Migrations
{
   /// <inheritdoc />
   public partial class configureUserPageTableName : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropForeignKey(
             name: "FK_AspNetUsers_UserPage_UserPageId",
             table: "AspNetUsers");

         migrationBuilder.DropForeignKey(
             name: "FK_UserPage_Organizations_OrganizationId",
             table: "UserPage");

         migrationBuilder.DropForeignKey(
             name: "FK_UserPage_Producers_ProducerId",
             table: "UserPage");

         migrationBuilder.DropPrimaryKey(
             name: "PK_UserPage",
             table: "UserPage");


         migrationBuilder.RenameTable(
             name: "UserPage",
             newName: "UsersPages");

         migrationBuilder.RenameIndex(
             name: "IX_UserPage_ProducerId",
             table: "UsersPages",
             newName: "IX_UsersPages_ProducerId");

         migrationBuilder.RenameIndex(
             name: "IX_UserPage_OrganizationId",
             table: "UsersPages",
             newName: "IX_UsersPages_OrganizationId");

         migrationBuilder.AddPrimaryKey(
             name: "PK_UsersPages",
             table: "UsersPages",
             column: "UserPageId");


         migrationBuilder.AddForeignKey(
             name: "FK_AspNetUsers_UsersPages_UserPageId",
             table: "AspNetUsers",
             column: "UserPageId",
             principalTable: "UsersPages",
             principalColumn: "UserPageId");

         migrationBuilder.AddForeignKey(
             name: "FK_UsersPages_Organizations_OrganizationId",
             table: "UsersPages",
             column: "OrganizationId",
             principalTable: "Organizations",
             principalColumn: "OrganizationId");

         migrationBuilder.AddForeignKey(
             name: "FK_UsersPages_Producers_ProducerId",
             table: "UsersPages",
             column: "ProducerId",
             principalTable: "Producers",
             principalColumn: "ProducerId");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropForeignKey(
             name: "FK_AspNetUsers_UsersPages_UserPageId",
             table: "AspNetUsers");

         migrationBuilder.DropForeignKey(
             name: "FK_UsersPages_Organizations_OrganizationId",
             table: "UsersPages");

         migrationBuilder.DropForeignKey(
             name: "FK_UsersPages_Producers_ProducerId",
             table: "UsersPages");

         migrationBuilder.DropPrimaryKey(
             name: "PK_UsersPages",
             table: "UsersPages");


         migrationBuilder.RenameTable(
             name: "UsersPages",
             newName: "UserPage");

         migrationBuilder.RenameIndex(
             name: "IX_UsersPages_ProducerId",
             table: "UserPage",
             newName: "IX_UserPage_ProducerId");

         migrationBuilder.RenameIndex(
             name: "IX_UsersPages_OrganizationId",
             table: "UserPage",
             newName: "IX_UserPage_OrganizationId");

         migrationBuilder.AddPrimaryKey(
             name: "PK_UserPage",
             table: "UserPage",
             column: "UserPageId");



         migrationBuilder.AddForeignKey(
             name: "FK_AspNetUsers_UserPage_UserPageId",
             table: "AspNetUsers",
             column: "UserPageId",
             principalTable: "UserPage",
             principalColumn: "UserPageId");

         migrationBuilder.AddForeignKey(
             name: "FK_UserPage_Organizations_OrganizationId",
             table: "UserPage",
             column: "OrganizationId",
             principalTable: "Organizations",
             principalColumn: "OrganizationId");

         migrationBuilder.AddForeignKey(
             name: "FK_UserPage_Producers_ProducerId",
             table: "UserPage",
             column: "ProducerId",
             principalTable: "Producers",
             principalColumn: "ProducerId");
      }
   }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventHub.EventManagement.Persistance.Migrations
{
   public partial class PreliminaryDB : Migration
   {
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.CreateTable(
             name: "Followers",
             columns: table => new
             {
                FollowerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Age = table.Column<int>(type: "int", nullable: false),
                Genre = table.Column<int>(type: "int", nullable: false),
                LiveIn = table.Column<string>(type: "nvarchar(max)", nullable: true)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Followers", x => x.FollowerId);
             });

         migrationBuilder.CreateTable(
             name: "Mediums",
             columns: table => new
             {
                MediumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Type = table.Column<int>(type: "int", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Mediums", x => x.MediumId);
             });

         migrationBuilder.CreateTable(
             name: "Organizations",
             columns: table => new
             {
                OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                BusinessType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                BusinessDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Organizations", x => x.OrganizationId);
             });

         migrationBuilder.CreateTable(
             name: "Producers",
             columns: table => new
             {
                ProducerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Age = table.Column<int>(type: "int", nullable: false),
                Genre = table.Column<int>(type: "int", nullable: false),
                LiveIn = table.Column<string>(type: "nvarchar(max)", nullable: true)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Producers", x => x.ProducerId);
             });

         migrationBuilder.CreateTable(
             name: "Categories",
             columns: table => new
             {
                CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                MediumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Categories", x => x.CategoryId);
                table.ForeignKey(
                       name: "FK_Categories_Mediums_MediumId",
                       column: x => x.MediumId,
                       principalTable: "Mediums",
                       principalColumn: "MediumId",
                       onDelete: ReferentialAction.Cascade);
             });

         migrationBuilder.CreateTable(
             name: "OrganizationsFollowers",
             columns: table => new
             {
                OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                FollowerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_OrganizationsFollowers", x => new { x.OrganizationId, x.FollowerId });
                table.ForeignKey(
                       name: "FK_OrganizationsFollowers_Followers_FollowerId",
                       column: x => x.FollowerId,
                       principalTable: "Followers",
                       principalColumn: "FollowerId",
                       onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                       name: "FK_OrganizationsFollowers_Organizations_OrganizationId",
                       column: x => x.OrganizationId,
                       principalTable: "Organizations",
                       principalColumn: "OrganizationId",
                       onDelete: ReferentialAction.Cascade);
             });

         migrationBuilder.CreateTable(
             name: "Speakers",
             columns: table => new
             {
                SpeakerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Genre = table.Column<int>(type: "int", nullable: false),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Speakers", x => x.SpeakerId);
                table.ForeignKey(
                       name: "FK_Speakers_Organizations_OrganizationId",
                       column: x => x.OrganizationId,
                       principalTable: "Organizations",
                       principalColumn: "OrganizationId",
                       onDelete: ReferentialAction.Cascade);
             });

         migrationBuilder.CreateTable(
             name: "ProducersFollowers",
             columns: table => new
             {
                ProducerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                FollowerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_ProducersFollowers", x => new { x.ProducerId, x.FollowerId });
                table.ForeignKey(
                       name: "FK_ProducersFollowers_Followers_FollowerId",
                       column: x => x.FollowerId,
                       principalTable: "Followers",
                       principalColumn: "FollowerId",
                       onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                       name: "FK_ProducersFollowers_Producers_ProducerId",
                       column: x => x.ProducerId,
                       principalTable: "Producers",
                       principalColumn: "ProducerId",
                       onDelete: ReferentialAction.Cascade);
             });

         migrationBuilder.CreateTable(
             name: "Events",
             columns: table => new
             {
                EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ProducerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Events", x => x.EventId);
                table.ForeignKey(
                       name: "FK_Events_Categories_CategoryId",
                       column: x => x.CategoryId,
                       principalTable: "Categories",
                       principalColumn: "CategoryId",
                       onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                       name: "FK_Events_Organizations_OrganizationId",
                       column: x => x.OrganizationId,
                       principalTable: "Organizations",
                       principalColumn: "OrganizationId",
                       onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                       name: "FK_Events_Producers_ProducerId",
                       column: x => x.ProducerId,
                       principalTable: "Producers",
                       principalColumn: "ProducerId",
                       onDelete: ReferentialAction.Cascade);
             });

         migrationBuilder.CreateTable(
             name: "Attendants",
             columns: table => new
             {
                AttendantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Age = table.Column<int>(type: "int", nullable: false),
                Genre = table.Column<int>(type: "int", nullable: false),
                LiveIn = table.Column<string>(type: "nvarchar(max)", nullable: true)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Attendants", x => x.AttendantId);
                table.ForeignKey(
                       name: "FK_Attendants_Events_EventId",
                       column: x => x.EventId,
                       principalTable: "Events",
                       principalColumn: "EventId",
                       onDelete: ReferentialAction.Cascade);
             });

         migrationBuilder.CreateTable(
             name: "OrganizationsEventsSpeakers",
             columns: table => new
             {
                OrganizationEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                SpeakerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_OrganizationsEventsSpeakers", x => new { x.OrganizationEventId, x.SpeakerId });
                table.ForeignKey(
                       name: "FK_OrganizationsEventsSpeakers_Events_OrganizationEventId",
                       column: x => x.OrganizationEventId,
                       principalTable: "Events",
                       principalColumn: "EventId",
                       onDelete: ReferentialAction.NoAction);
                table.ForeignKey(
                       name: "FK_OrganizationsEventsSpeakers_Speakers_SpeakerId",
                       column: x => x.SpeakerId,
                       principalTable: "Speakers",
                       principalColumn: "SpeakerId",
                       onDelete: ReferentialAction.Cascade);
             });

         migrationBuilder.CreateIndex(
             name: "IX_Attendants_EventId",
             table: "Attendants",
             column: "EventId");

         migrationBuilder.CreateIndex(
             name: "IX_Categories_MediumId",
             table: "Categories",
             column: "MediumId");

         migrationBuilder.CreateIndex(
             name: "IX_Events_CategoryId",
             table: "Events",
             column: "CategoryId");

         migrationBuilder.CreateIndex(
             name: "IX_Events_OrganizationId",
             table: "Events",
             column: "OrganizationId");

         migrationBuilder.CreateIndex(
             name: "IX_Events_ProducerId",
             table: "Events",
             column: "ProducerId");

         migrationBuilder.CreateIndex(
             name: "IX_OrganizationsEventsSpeakers_SpeakerId",
             table: "OrganizationsEventsSpeakers",
             column: "SpeakerId");

         migrationBuilder.CreateIndex(
             name: "IX_OrganizationsFollowers_FollowerId",
             table: "OrganizationsFollowers",
             column: "FollowerId");

         migrationBuilder.CreateIndex(
             name: "IX_ProducersFollowers_FollowerId",
             table: "ProducersFollowers",
             column: "FollowerId");

         migrationBuilder.CreateIndex(
             name: "IX_Speakers_OrganizationId",
             table: "Speakers",
             column: "OrganizationId");
      }

      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropTable(
             name: "Attendants");

         migrationBuilder.DropTable(
             name: "OrganizationsEventsSpeakers");

         migrationBuilder.DropTable(
             name: "OrganizationsFollowers");

         migrationBuilder.DropTable(
             name: "ProducersFollowers");

         migrationBuilder.DropTable(
             name: "Events");

         migrationBuilder.DropTable(
             name: "Speakers");

         migrationBuilder.DropTable(
             name: "Followers");

         migrationBuilder.DropTable(
             name: "Categories");

         migrationBuilder.DropTable(
             name: "Producers");

         migrationBuilder.DropTable(
             name: "Organizations");

         migrationBuilder.DropTable(
             name: "Mediums");
      }
   }
}

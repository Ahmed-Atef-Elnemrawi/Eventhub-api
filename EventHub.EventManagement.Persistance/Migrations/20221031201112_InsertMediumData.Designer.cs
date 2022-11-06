﻿// <auto-generated />
using System;
using EventHub.EventManagement.Presistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventHub.EventManagement.Persistance.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20221031201112_InsertMediumData")]
    partial class InsertMediumData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.EventEntities.Attendant", b =>
                {
                    b.Property<Guid>("AttendantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LiveIn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AttendantId");

                    b.HasIndex("EventId");

                    b.ToTable("Attendants", (string)null);
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.EventEntities.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MediumId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.HasIndex("MediumId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.EventEntities.Event", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EventId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Events");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Event");
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.EventEntities.Medium", b =>
                {
                    b.Property<Guid>("MediumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("MediumId");

                    b.ToTable("Mediums");

                    b.HasData(
                        new
                        {
                            MediumId = new Guid("07ba7806-dad3-447f-8233-564fbc6c1010"),
                            Type = 0
                        },
                        new
                        {
                            MediumId = new Guid("ff1b224d-a32e-473b-9684-df0493140094"),
                            Type = 2
                        },
                        new
                        {
                            MediumId = new Guid("581b8af3-81eb-4d29-865c-2686b6a0ce9b"),
                            Type = 1
                        },
                        new
                        {
                            MediumId = new Guid("6691efdb-7f06-4da1-a31b-c29912c49ac4"),
                            Type = 3
                        });
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.Follower", b =>
                {
                    b.Property<Guid>("FollowerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LiveIn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FollowerId");

                    b.ToTable("Followers", (string)null);
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.OrganizationEntities.Organization", b =>
                {
                    b.Property<Guid>("OrganizationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BusinessDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrganizationId");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.OrganizationEntities.OrganizationEventSpeaker", b =>
                {
                    b.Property<Guid>("OrganizationEventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SpeakerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OrganizationEventId", "SpeakerId");

                    b.HasIndex("SpeakerId");

                    b.ToTable("OrganizationsEventsSpeakers", (string)null);
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.OrganizationEntities.OrganizationFollower", b =>
                {
                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FollowerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OrganizationId", "FollowerId");

                    b.HasIndex("FollowerId");

                    b.ToTable("OrganizationsFollowers", (string)null);
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.OrganizationEntities.Speaker", b =>
                {
                    b.Property<Guid>("SpeakerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<string>("JobTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SpeakerId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Speakers", (string)null);
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.ProducerEntities.Producer", b =>
                {
                    b.Property<Guid>("ProducerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LiveIn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProducerId");

                    b.ToTable("Producers", (string)null);
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.ProducerEntities.ProducerFollower", b =>
                {
                    b.Property<Guid>("ProducerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FollowerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProducerId", "FollowerId");

                    b.HasIndex("FollowerId");

                    b.ToTable("ProducersFollowers", (string)null);
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.OrganizationEntities.OrganizationEvent", b =>
                {
                    b.HasBaseType("EventHub.EventManagement.Domain.Entities.EventEntities.Event");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("OrganizationId");

                    b.HasDiscriminator().HasValue("OrganizationEvent");
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.ProducerEntities.ProducerEvent", b =>
                {
                    b.HasBaseType("EventHub.EventManagement.Domain.Entities.EventEntities.Event");

                    b.Property<Guid>("ProducerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("ProducerId");

                    b.HasDiscriminator().HasValue("ProducerEvent");
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.EventEntities.Attendant", b =>
                {
                    b.HasOne("EventHub.EventManagement.Domain.Entities.EventEntities.Event", "Event")
                        .WithMany("Attendants")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.EventEntities.Category", b =>
                {
                    b.HasOne("EventHub.EventManagement.Domain.Entities.EventEntities.Medium", "Medium")
                        .WithMany("Categories")
                        .HasForeignKey("MediumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medium");
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.EventEntities.Event", b =>
                {
                    b.HasOne("EventHub.EventManagement.Domain.Entities.EventEntities.Category", "Category")
                        .WithMany("Events")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.OrganizationEntities.OrganizationEventSpeaker", b =>
                {
                    b.HasOne("EventHub.EventManagement.Domain.Entities.OrganizationEntities.OrganizationEvent", null)
                        .WithMany()
                        .HasForeignKey("OrganizationEventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventHub.EventManagement.Domain.Entities.OrganizationEntities.Speaker", null)
                        .WithMany()
                        .HasForeignKey("SpeakerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.OrganizationEntities.OrganizationFollower", b =>
                {
                    b.HasOne("EventHub.EventManagement.Domain.Entities.Follower", null)
                        .WithMany()
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventHub.EventManagement.Domain.Entities.OrganizationEntities.Organization", null)
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.OrganizationEntities.Speaker", b =>
                {
                    b.HasOne("EventHub.EventManagement.Domain.Entities.OrganizationEntities.Organization", "Organization")
                        .WithMany("Speakers")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.ProducerEntities.ProducerFollower", b =>
                {
                    b.HasOne("EventHub.EventManagement.Domain.Entities.Follower", null)
                        .WithMany()
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventHub.EventManagement.Domain.Entities.ProducerEntities.Producer", null)
                        .WithMany()
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.OrganizationEntities.OrganizationEvent", b =>
                {
                    b.HasOne("EventHub.EventManagement.Domain.Entities.OrganizationEntities.Organization", "Organization")
                        .WithMany("OrganizationEvents")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.ProducerEntities.ProducerEvent", b =>
                {
                    b.HasOne("EventHub.EventManagement.Domain.Entities.ProducerEntities.Producer", "Producer")
                        .WithMany("Events")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producer");
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.EventEntities.Category", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.EventEntities.Event", b =>
                {
                    b.Navigation("Attendants");
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.EventEntities.Medium", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.OrganizationEntities.Organization", b =>
                {
                    b.Navigation("OrganizationEvents");

                    b.Navigation("Speakers");
                });

            modelBuilder.Entity("EventHub.EventManagement.Domain.Entities.ProducerEntities.Producer", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}

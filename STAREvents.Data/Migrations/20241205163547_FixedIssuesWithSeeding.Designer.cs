﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using STAREvents.Web.Data;

#nullable disable

namespace STAREvents.Data.Migrations
{
    [DbContext(typeof(STAREventsDbContext))]
    [Migration("20241205163547_FixedIssuesWithSeeding")]
    partial class FixedIssuesWithSeeding
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("STAREvents.Data.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<byte[]>("ProfilePicture")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("STAREvents.Data.Models.Category", b =>
                {
                    b.Property<Guid>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryID = new Guid("d3b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Music"
                        },
                        new
                        {
                            CategoryID = new Guid("e4b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Sports"
                        },
                        new
                        {
                            CategoryID = new Guid("f5b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Technology"
                        },
                        new
                        {
                            CategoryID = new Guid("a6b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Education"
                        },
                        new
                        {
                            CategoryID = new Guid("b7b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Health"
                        },
                        new
                        {
                            CategoryID = new Guid("c8b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Art"
                        },
                        new
                        {
                            CategoryID = new Guid("d9b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Business"
                        },
                        new
                        {
                            CategoryID = new Guid("eab07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Science"
                        },
                        new
                        {
                            CategoryID = new Guid("fbb07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Comedy"
                        },
                        new
                        {
                            CategoryID = new Guid("acb07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Travel"
                        },
                        new
                        {
                            CategoryID = new Guid("bdb07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Lifestyle"
                        },
                        new
                        {
                            CategoryID = new Guid("cec07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Fitness"
                        },
                        new
                        {
                            CategoryID = new Guid("dfd07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Gaming"
                        },
                        new
                        {
                            CategoryID = new Guid("e0e07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Cooking"
                        },
                        new
                        {
                            CategoryID = new Guid("f1f07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "History"
                        },
                        new
                        {
                            CategoryID = new Guid("a2a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Politics"
                        },
                        new
                        {
                            CategoryID = new Guid("b3b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Finance"
                        },
                        new
                        {
                            CategoryID = new Guid("c4c07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Nature"
                        },
                        new
                        {
                            CategoryID = new Guid("d5d07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Photography"
                        },
                        new
                        {
                            CategoryID = new Guid("e6e07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Writing"
                        },
                        new
                        {
                            CategoryID = new Guid("f7f07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Dance"
                        },
                        new
                        {
                            CategoryID = new Guid("a8a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Film"
                        },
                        new
                        {
                            CategoryID = new Guid("b9b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Literature"
                        },
                        new
                        {
                            CategoryID = new Guid("cac07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Environment"
                        },
                        new
                        {
                            CategoryID = new Guid("dbd07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Social"
                        },
                        new
                        {
                            CategoryID = new Guid("ece07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Community"
                        },
                        new
                        {
                            CategoryID = new Guid("fdf07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Theatre"
                        },
                        new
                        {
                            CategoryID = new Guid("a0a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Networking"
                        },
                        new
                        {
                            CategoryID = new Guid("b1b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Charity"
                        },
                        new
                        {
                            CategoryID = new Guid("c2c07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                            Name = "Hobbies"
                        });
                });

            modelBuilder.Entity("STAREvents.Data.Models.Comment", b =>
                {
                    b.Property<Guid>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("PostedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CommentId");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("STAREvents.Data.Models.Event", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<Guid>("CategoryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("NumberOfParticipants")
                        .HasColumnType("int")
                        .HasComment("This represents how many users have joined the event.");

                    b.Property<Guid>("OrganizerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit")
                        .HasComment("Shows wether the event has been deleted");

                    b.HasKey("EventId");

                    b.HasIndex("CategoryID");

                    b.HasIndex("OrganizerID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("STAREvents.Data.Models.EventCategory", b =>
                {
                    b.Property<Guid>("EventID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EventID", "CategoryID");

                    b.HasIndex("CategoryID");

                    b.ToTable("EventsCategories");
                });

            modelBuilder.Entity("STAREvents.Data.Models.UserEventAttendance", b =>
                {
                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("JoinedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("EventId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersEventAttendances");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("STAREvents.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("STAREvents.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("STAREvents.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("STAREvents.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("STAREvents.Data.Models.Comment", b =>
                {
                    b.HasOne("STAREvents.Data.Models.Event", "Event")
                        .WithMany("EventComments")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("STAREvents.Data.Models.ApplicationUser", "User")
                        .WithMany("UserComments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("STAREvents.Data.Models.Event", b =>
                {
                    b.HasOne("STAREvents.Data.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("STAREvents.Data.Models.ApplicationUser", "Organizer")
                        .WithMany("OrganizedEvents")
                        .HasForeignKey("OrganizerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("STAREvents.Data.Models.EventCategory", b =>
                {
                    b.HasOne("STAREvents.Data.Models.Category", "Category")
                        .WithMany("EventCategories")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("STAREvents.Data.Models.Event", "Event")
                        .WithMany("EventCategories")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("STAREvents.Data.Models.UserEventAttendance", b =>
                {
                    b.HasOne("STAREvents.Data.Models.Event", "Event")
                        .WithMany("UserEventAttendances")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("STAREvents.Data.Models.ApplicationUser", "User")
                        .WithMany("UserEventAttendances")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("STAREvents.Data.Models.ApplicationUser", b =>
                {
                    b.Navigation("OrganizedEvents");

                    b.Navigation("UserComments");

                    b.Navigation("UserEventAttendances");
                });

            modelBuilder.Entity("STAREvents.Data.Models.Category", b =>
                {
                    b.Navigation("EventCategories");
                });

            modelBuilder.Entity("STAREvents.Data.Models.Event", b =>
                {
                    b.Navigation("EventCategories");

                    b.Navigation("EventComments");

                    b.Navigation("UserEventAttendances");
                });
#pragma warning restore 612, 618
        }
    }
}

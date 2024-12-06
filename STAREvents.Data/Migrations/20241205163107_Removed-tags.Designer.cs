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
    [Migration("20241205163107_Removed-tags")]
    partial class Removedtags
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
                            CategoryID = new Guid("e9b7be08-b33e-476a-88ee-cf088bea38a4"),
                            Name = "Music"
                        },
                        new
                        {
                            CategoryID = new Guid("f8265891-51e1-47ae-a846-f14bcdf42e8c"),
                            Name = "Sports"
                        },
                        new
                        {
                            CategoryID = new Guid("ee63f2cd-f9e8-49e2-a4ff-8c8eabcf0b59"),
                            Name = "Technology"
                        },
                        new
                        {
                            CategoryID = new Guid("66962860-7fb2-43e8-8421-59e02e9e5868"),
                            Name = "Education"
                        },
                        new
                        {
                            CategoryID = new Guid("0417e57b-03bd-41bf-ba6a-183faab813c8"),
                            Name = "Health"
                        },
                        new
                        {
                            CategoryID = new Guid("a12fd689-9a73-4f31-a63d-2deabd46f0ab"),
                            Name = "Art"
                        },
                        new
                        {
                            CategoryID = new Guid("d83d7fed-1665-416d-a912-69a4413f2b89"),
                            Name = "Business"
                        },
                        new
                        {
                            CategoryID = new Guid("7d90f0a3-ab78-43ab-a8b2-1bac1994c9c9"),
                            Name = "Science"
                        },
                        new
                        {
                            CategoryID = new Guid("5a1c8b11-fcaf-48ff-a8e2-60a22e3f49d7"),
                            Name = "Comedy"
                        },
                        new
                        {
                            CategoryID = new Guid("8eec4317-2bf3-409d-a6de-a02fb637288c"),
                            Name = "Travel"
                        },
                        new
                        {
                            CategoryID = new Guid("bc786c75-4c9b-4b62-a71a-3e0804231fc3"),
                            Name = "Lifestyle"
                        },
                        new
                        {
                            CategoryID = new Guid("8b4c824a-41a6-443b-a144-1ee46ce20fc2"),
                            Name = "Fitness"
                        },
                        new
                        {
                            CategoryID = new Guid("79e77c2d-7def-41f1-bcc0-5e8fda0a3169"),
                            Name = "Gaming"
                        },
                        new
                        {
                            CategoryID = new Guid("bbb80a03-e3c3-4782-9097-c0e806c3117c"),
                            Name = "Cooking"
                        },
                        new
                        {
                            CategoryID = new Guid("5dc8e075-02bc-40c0-89a8-2c5e0336ca2e"),
                            Name = "History"
                        },
                        new
                        {
                            CategoryID = new Guid("5f0dff34-416f-4441-9105-d5eaae2a7e3c"),
                            Name = "Politics"
                        },
                        new
                        {
                            CategoryID = new Guid("1ba48aab-06d1-4513-8d1a-9ca6e2150011"),
                            Name = "Finance"
                        },
                        new
                        {
                            CategoryID = new Guid("3b4f39ef-b91a-4c21-94af-ffea46994078"),
                            Name = "Nature"
                        },
                        new
                        {
                            CategoryID = new Guid("1169ae12-5a7e-48a7-a6d6-2a1a7e1c0be2"),
                            Name = "Photography"
                        },
                        new
                        {
                            CategoryID = new Guid("fde62827-21e1-4b3f-873a-cce9ac1c80aa"),
                            Name = "Writing"
                        },
                        new
                        {
                            CategoryID = new Guid("37e0ac3a-02b7-4efc-9af3-5b57f5c2dd77"),
                            Name = "Dance"
                        },
                        new
                        {
                            CategoryID = new Guid("8e13109e-b0c8-4984-b584-d7ed446920ac"),
                            Name = "Film"
                        },
                        new
                        {
                            CategoryID = new Guid("b2bcb1d8-6559-401d-a41d-4f5bfa60be2a"),
                            Name = "Literature"
                        },
                        new
                        {
                            CategoryID = new Guid("ea5adb99-1fbb-41ff-b464-5d4ceb20960a"),
                            Name = "Environment"
                        },
                        new
                        {
                            CategoryID = new Guid("25f0cf63-26d8-440d-8f26-12177d935429"),
                            Name = "Social"
                        },
                        new
                        {
                            CategoryID = new Guid("2822802b-a6a2-4e01-a2d2-ed8dbb1b5f44"),
                            Name = "Community"
                        },
                        new
                        {
                            CategoryID = new Guid("b20962b2-8c64-4a0e-b44c-0f3859f293b9"),
                            Name = "Theatre"
                        },
                        new
                        {
                            CategoryID = new Guid("6a1ccd21-9719-45ea-9b20-ef1610802dd6"),
                            Name = "Networking"
                        },
                        new
                        {
                            CategoryID = new Guid("0fbc3c69-cae3-4747-82a3-c8f2bc841570"),
                            Name = "Charity"
                        },
                        new
                        {
                            CategoryID = new Guid("387756b4-019f-47ef-9faa-2ea83cf75773"),
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
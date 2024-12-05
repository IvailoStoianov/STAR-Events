using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace STAREvents.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Capacity", "CategoryID", "CreatedOnDate", "Description", "EndDate", "ImageUrl", "Name", "NumberOfParticipants", "OrganizerID", "StartDate", "isDeleted" },
                values: new object[,]
                {
                    { new Guid("a1a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), 100, new Guid("a0a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A great opportunity to network with professionals.", new DateTime(2023, 11, 1, 17, 0, 0, 0, DateTimeKind.Unspecified), "/images/image-not-found-event.png", "Networking Event", 50, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 11, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("a2a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), 200, new Guid("a2a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), new DateTime(2023, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Join us for a lively political debate.", new DateTime(2023, 11, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), "/images/image-not-found-event.png", "Political Debate", 150, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 11, 2, 10, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("a3a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), 150, new Guid("a8a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), new DateTime(2023, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Watch the latest blockbuster with us.", new DateTime(2023, 11, 3, 21, 0, 0, 0, DateTimeKind.Unspecified), "/images/image-not-found-event.png", "Film Screening", 100, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 11, 3, 18, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("a4a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), 50, new Guid("a6b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), new DateTime(2023, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enhance your skills with our workshop.", new DateTime(2023, 11, 4, 15, 0, 0, 0, DateTimeKind.Unspecified), "/images/image-not-found-event.png", "Educational Workshop", 30, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 11, 4, 9, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("a5a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), 80, new Guid("acb07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), new DateTime(2023, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Learn about exciting travel destinations.", new DateTime(2023, 11, 5, 16, 0, 0, 0, DateTimeKind.Unspecified), "/images/image-not-found-event.png", "Travel Seminar", 60, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 11, 5, 14, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("a6a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), 120, new Guid("b1b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Support a good cause at our fundraiser.", new DateTime(2023, 11, 6, 22, 0, 0, 0, DateTimeKind.Unspecified), "/images/image-not-found-event.png", "Charity Fundraiser", 100, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 11, 6, 19, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("a7a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), 70, new Guid("b3b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), new DateTime(2023, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Improve your financial literacy.", new DateTime(2023, 11, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), "/images/image-not-found-event.png", "Finance Workshop", 50, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 11, 7, 10, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("a8a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), 90, new Guid("b7b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), new DateTime(2023, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Learn about health and wellness.", new DateTime(2023, 11, 8, 15, 0, 0, 0, DateTimeKind.Unspecified), "/images/image-not-found-event.png", "Health Seminar", 70, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 11, 8, 13, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("a9a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), 60, new Guid("b9b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), new DateTime(2023, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Discuss your favorite books.", new DateTime(2023, 11, 9, 19, 0, 0, 0, DateTimeKind.Unspecified), "/images/image-not-found-event.png", "Literature Club", 40, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 11, 9, 17, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("b0a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), 200, new Guid("bdb07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Explore the latest lifestyle trends.", new DateTime(2023, 11, 10, 17, 0, 0, 0, DateTimeKind.Unspecified), "/images/image-not-found-event.png", "Lifestyle Expo", 150, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 11, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("b1a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), 100, new Guid("c8b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), new DateTime(2023, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "View stunning artworks.", new DateTime(2023, 11, 11, 18, 0, 0, 0, DateTimeKind.Unspecified), "/images/image-not-found-event.png", "Art Exhibition", 80, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 11, 11, 10, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("b2a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), 300, new Guid("d3b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), new DateTime(2023, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enjoy live music performances.", new DateTime(2023, 11, 12, 22, 0, 0, 0, DateTimeKind.Unspecified), "/images/image-not-found-event.png", "Music Concert", 250, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 11, 12, 19, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("b3a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), 150, new Guid("d9b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), new DateTime(2023, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Network with business professionals.", new DateTime(2023, 11, 13, 17, 0, 0, 0, DateTimeKind.Unspecified), "/images/image-not-found-event.png", "Business Conference", 120, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 11, 13, 9, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("b4a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), 200, new Guid("e4b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), new DateTime(2023, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Compete in our sports tournament.", new DateTime(2023, 11, 14, 18, 0, 0, 0, DateTimeKind.Unspecified), "/images/image-not-found-event.png", "Sports Tournament", 180, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 11, 14, 8, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("b5a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), 100, new Guid("eab07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), new DateTime(2023, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Explore scientific discoveries.", new DateTime(2023, 11, 15, 16, 0, 0, 0, DateTimeKind.Unspecified), "/images/image-not-found-event.png", "Science Fair", 90, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 11, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("b6a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), 150, new Guid("f5b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), new DateTime(2023, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Discover the latest in technology.", new DateTime(2023, 11, 16, 17, 0, 0, 0, DateTimeKind.Unspecified), "/images/image-not-found-event.png", "Tech Expo", 130, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 11, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("b7a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), 100, new Guid("fbb07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), new DateTime(2023, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Laugh out loud at our comedy show.", new DateTime(2023, 11, 17, 21, 0, 0, 0, DateTimeKind.Unspecified), "/images/image-not-found-event.png", "Comedy Show", 80, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 11, 17, 19, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("b8a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), 50, new Guid("c2c07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), new DateTime(2023, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Share your hobbies with others.", new DateTime(2023, 11, 18, 16, 0, 0, 0, DateTimeKind.Unspecified), "/images/image-not-found-event.png", "Hobbies Meetup", 40, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 11, 18, 14, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("b9a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), 30, new Guid("c4c07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enjoy a walk in nature.", new DateTime(2023, 11, 19, 10, 0, 0, 0, DateTimeKind.Unspecified), "/images/image-not-found-event.png", "Nature Walk", 20, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 11, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a1a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a2a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a3a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a4a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a5a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a6a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a7a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a8a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a9a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b0a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b1a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b2a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b3a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b4a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b5a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b6a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b7a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b8a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b9a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));
        }
    }
}

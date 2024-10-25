using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace STAREvents.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingOrganizersCategoriesVenues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Events",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "ApplicationUser",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "Name" },
                values: new object[,]
                {
                    { 1, "Music" },
                    { 2, "Sports" },
                    { 3, "Technology" },
                    { 4, "Education" },
                    { 5, "Health" },
                    { 6, "Art" },
                    { 7, "Business" },
                    { 8, "Science" },
                    { 9, "Comedy" },
                    { 10, "Travel" },
                    { 11, "Lifestyle" },
                    { 12, "Fitness" },
                    { 13, "Gaming" },
                    { 14, "Cooking" },
                    { 15, "History" },
                    { 16, "Politics" },
                    { 17, "Finance" },
                    { 18, "Nature" },
                    { 19, "Photography" },
                    { 20, "Writing" },
                    { 21, "Dance" },
                    { 22, "Film" },
                    { 23, "Literature" },
                    { 24, "Environment" },
                    { 25, "Social" },
                    { 26, "Community" },
                    { 27, "Theatre" },
                    { 28, "Networking" },
                    { 29, "Charity" },
                    { 30, "Hobbies" }
                });

            migrationBuilder.InsertData(
                table: "Organizers",
                columns: new[] { "OrganizerID", "ContactInfo", "Name" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "John Doe" },
                    { 2, "jane.smith@example.com", "Jane Smith" },
                    { 3, "emily.johnson@example.com", "Emily Johnson" },
                    { 4, "michael.brown@example.com", "Michael Brown" },
                    { 5, "sarah.wilson@example.com", "Sarah Wilson" },
                    { 6, "david.martinez@example.com", "David Martinez" },
                    { 7, "chris.lee@example.com", "Chris Lee" },
                    { 8, "jessica.white@example.com", "Jessica White" },
                    { 9, "daniel.harris@example.com", "Daniel Harris" },
                    { 10, "laura.thompson@example.com", "Laura Thompson" },
                    { 11, "tom.clark@example.com", "Tom Clark" },
                    { 12, "anna.lewis@example.com", "Anna Lewis" },
                    { 13, "robert.walker@example.com", "Robert Walker" },
                    { 14, "lisa.hall@example.com", "Lisa Hall" },
                    { 15, "mark.allen@example.com", "Mark Allen" },
                    { 16, "sophie.young@example.com", "Sophie Young" },
                    { 17, "james.hernandez@example.com", "James Hernandez" },
                    { 18, "emma.king@example.com", "Emma King" },
                    { 19, "brian.wright@example.com", "Brian Wright" },
                    { 20, "olivia.scott@example.com", "Olivia Scott" },
                    { 21, "kevin.green@example.com", "Kevin Green" },
                    { 22, "evelyn.adams@example.com", "Evelyn Adams" },
                    { 23, "jason.baker@example.com", "Jason Baker" },
                    { 24, "isabella.nelson@example.com", "Isabella Nelson" },
                    { 25, "ryan.carter@example.com", "Ryan Carter" },
                    { 26, "grace.mitchell@example.com", "Grace Mitchell" },
                    { 27, "ethan.perez@example.com", "Ethan Perez" },
                    { 28, "charlotte.roberts@example.com", "Charlotte Roberts" },
                    { 29, "henry.turner@example.com", "Henry Turner" },
                    { 30, "sofia.phillips@example.com", "Sofia Phillips" }
                });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "VenueID", "Capacity", "Location", "Name" },
                values: new object[,]
                {
                    { 1, 500, "Main Street", "City Hall" },
                    { 2, 1000, "Central Park", "Sports Arena" },
                    { 3, 300, "Broadway", "Downtown Theater" },
                    { 4, 800, "1st Avenue", "Conference Center" },
                    { 5, 2000, "West End", "Open Air Stadium" },
                    { 6, 200, "Museum District", "Art Gallery" },
                    { 7, 150, "East Side", "Community Hall" },
                    { 8, 1000, "Silicon Valley", "Tech Park" },
                    { 9, 400, "5th Street", "Music Club" },
                    { 10, 700, "Downtown", "Grand Ballroom" },
                    { 11, 1200, "College Road", "University Auditorium" },
                    { 12, 300, "Lakeside", "Lakeview Pavilion" },
                    { 13, 5000, "South Park", "City Park" },
                    { 14, 100, "Downtown Heights", "Skyscraper Rooftop" },
                    { 15, 400, "Library Avenue", "National Library" },
                    { 16, 600, "Science Blvd", "Science Museum" },
                    { 17, 200, "Greenway", "Botanical Garden" },
                    { 18, 1000, "Opera Lane", "Opera House" },
                    { 19, 2500, "Expo Road", "Convention Hall" },
                    { 20, 1500, "Rock Valley", "Amphitheater" },
                    { 21, 300, "School Street", "City High School Gym" },
                    { 22, 1000, "Town Center", "Town Square" },
                    { 23, 600, "Downtown Circle", "Main Event Plaza" },
                    { 24, 500, "Beachside", "Luxury Resort" },
                    { 25, 1200, "North Ridge", "Indoor Sports Complex" },
                    { 26, 200, "Central Station", "Railway Auditorium" },
                    { 27, 5000, "North Edge", "Open Grounds" },
                    { 28, 150, "Vintage Road", "Old Town Theater" },
                    { 29, 350, "Fitness Avenue", "Fitness Center Arena" },
                    { 30, 800, "Event Street", "Downtown Events Hall" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "OrganizerID",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 30);

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "ApplicationUser");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldNullable: true);
        }
    }
}

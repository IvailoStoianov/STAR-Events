using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STAREvents.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEventsDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a1a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 1, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 1, 9, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a2a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 2, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a3a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 3, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 3, 18, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a4a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 4, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 4, 9, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a5a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 5, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 5, 14, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a6a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 6, 22, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 6, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a7a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 7, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a8a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 8, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 8, 13, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a9a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 9, 19, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 9, 17, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b0a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 10, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 10, 9, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b1a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 11, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 11, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b2a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 12, 22, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 12, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b3a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 13, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 13, 9, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b4a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 14, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 14, 8, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b5a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 15, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 15, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b6a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 16, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 16, 9, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b7a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 17, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 17, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b8a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 18, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 18, 14, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b9a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 19, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 19, 8, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a1a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 1, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 1, 9, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a2a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 2, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a3a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 3, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 3, 18, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a4a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 4, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 4, 9, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a5a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 5, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 5, 14, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a6a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 6, 22, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 6, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a7a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 7, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a8a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 8, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 8, 13, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a9a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 9, 19, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 9, 17, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b0a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 10, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 10, 9, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b1a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 11, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 11, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b2a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 12, 22, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 12, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b3a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 13, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 13, 9, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b4a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 14, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 14, 8, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b5a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 15, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 15, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b6a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 16, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 16, 9, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b7a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 17, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 17, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b8a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 18, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 18, 14, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b9a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                columns: new[] { "CreatedOnDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 19, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 19, 8, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}

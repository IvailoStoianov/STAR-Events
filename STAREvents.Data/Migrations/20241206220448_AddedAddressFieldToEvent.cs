using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STAREvents.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedAddressFieldToEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Events",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a1a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                column: "Address",
                value: "Ndk, Bulgaria Blvd, 1463 Sofia");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a2a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                column: "Address",
                value: "Ndk, Bulgaria Blvd, 1463 Sofia");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a3a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                column: "Address",
                value: "Ndk, Bulgaria Blvd, 1463 Sofia");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a4a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                column: "Address",
                value: "Ndk, Bulgaria Blvd, 1463 Sofia");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a5a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                column: "Address",
                value: "Ndk, Bulgaria Blvd, 1463 Sofia");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a6a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                column: "Address",
                value: "Ndk, Bulgaria Blvd, 1463 Sofia");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a7a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                column: "Address",
                value: "Ndk, Bulgaria Blvd, 1463 Sofia");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a8a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                column: "Address",
                value: "Ndk, Bulgaria Blvd, 1463 Sofia");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("a9a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                column: "Address",
                value: "Ndk, Bulgaria Blvd, 1463 Sofia");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b0a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                column: "Address",
                value: "Ndk, Bulgaria Blvd, 1463 Sofia");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b1a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                column: "Address",
                value: "Ndk, Bulgaria Blvd, 1463 Sofia");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b2a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                column: "Address",
                value: "Ndk, Bulgaria Blvd, 1463 Sofia");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b3a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                column: "Address",
                value: "Ndk, Bulgaria Blvd, 1463 Sofia");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b4a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                column: "Address",
                value: "Ndk, Bulgaria Blvd, 1463 Sofia");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b5a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                column: "Address",
                value: "Ndk, Bulgaria Blvd, 1463 Sofia");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b6a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                column: "Address",
                value: "Ndk, Bulgaria Blvd, 1463 Sofia");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b7a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                column: "Address",
                value: "Ndk, Bulgaria Blvd, 1463 Sofia");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b8a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                column: "Address",
                value: "Ndk, Bulgaria Blvd, 1463 Sofia");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b9a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"),
                column: "Address",
                value: "Ndk, Bulgaria Blvd, 1463 Sofia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Events");
        }
    }
}

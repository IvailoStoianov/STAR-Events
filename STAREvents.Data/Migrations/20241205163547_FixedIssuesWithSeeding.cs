using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace STAREvents.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedIssuesWithSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("0417e57b-03bd-41bf-ba6a-183faab813c8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("0fbc3c69-cae3-4747-82a3-c8f2bc841570"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("1169ae12-5a7e-48a7-a6d6-2a1a7e1c0be2"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("1ba48aab-06d1-4513-8d1a-9ca6e2150011"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("25f0cf63-26d8-440d-8f26-12177d935429"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("2822802b-a6a2-4e01-a2d2-ed8dbb1b5f44"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("37e0ac3a-02b7-4efc-9af3-5b57f5c2dd77"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("387756b4-019f-47ef-9faa-2ea83cf75773"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("3b4f39ef-b91a-4c21-94af-ffea46994078"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("5a1c8b11-fcaf-48ff-a8e2-60a22e3f49d7"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("5dc8e075-02bc-40c0-89a8-2c5e0336ca2e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("5f0dff34-416f-4441-9105-d5eaae2a7e3c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("66962860-7fb2-43e8-8421-59e02e9e5868"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("6a1ccd21-9719-45ea-9b20-ef1610802dd6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("79e77c2d-7def-41f1-bcc0-5e8fda0a3169"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("7d90f0a3-ab78-43ab-a8b2-1bac1994c9c9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("8b4c824a-41a6-443b-a144-1ee46ce20fc2"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("8e13109e-b0c8-4984-b584-d7ed446920ac"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("8eec4317-2bf3-409d-a6de-a02fb637288c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("a12fd689-9a73-4f31-a63d-2deabd46f0ab"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("b20962b2-8c64-4a0e-b44c-0f3859f293b9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("b2bcb1d8-6559-401d-a41d-4f5bfa60be2a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("bbb80a03-e3c3-4782-9097-c0e806c3117c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("bc786c75-4c9b-4b62-a71a-3e0804231fc3"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("d83d7fed-1665-416d-a912-69a4413f2b89"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("e9b7be08-b33e-476a-88ee-cf088bea38a4"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("ea5adb99-1fbb-41ff-b464-5d4ceb20960a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("ee63f2cd-f9e8-49e2-a4ff-8c8eabcf0b59"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("f8265891-51e1-47ae-a846-f14bcdf42e8c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("fde62827-21e1-4b3f-873a-cce9ac1c80aa"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "Name" },
                values: new object[,]
                {
                    { new Guid("a0a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Networking" },
                    { new Guid("a2a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Politics" },
                    { new Guid("a6b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Education" },
                    { new Guid("a8a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Film" },
                    { new Guid("acb07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Travel" },
                    { new Guid("b1b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Charity" },
                    { new Guid("b3b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Finance" },
                    { new Guid("b7b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Health" },
                    { new Guid("b9b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Literature" },
                    { new Guid("bdb07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Lifestyle" },
                    { new Guid("c2c07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Hobbies" },
                    { new Guid("c4c07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Nature" },
                    { new Guid("c8b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Art" },
                    { new Guid("cac07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Environment" },
                    { new Guid("cec07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Fitness" },
                    { new Guid("d3b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Music" },
                    { new Guid("d5d07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Photography" },
                    { new Guid("d9b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Business" },
                    { new Guid("dbd07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Social" },
                    { new Guid("dfd07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Gaming" },
                    { new Guid("e0e07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Cooking" },
                    { new Guid("e4b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Sports" },
                    { new Guid("e6e07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Writing" },
                    { new Guid("eab07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Science" },
                    { new Guid("ece07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Community" },
                    { new Guid("f1f07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "History" },
                    { new Guid("f5b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Technology" },
                    { new Guid("f7f07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Dance" },
                    { new Guid("fbb07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Comedy" },
                    { new Guid("fdf07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"), "Theatre" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("a0a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("a2a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("a6b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("a8a07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("acb07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("b1b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("b3b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("b7b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("b9b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("bdb07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("c2c07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("c4c07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("c8b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("cac07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("cec07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("d3b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("d5d07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("d9b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("dbd07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("dfd07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("e0e07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("e4b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("e6e07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("eab07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("ece07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("f1f07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("f5b07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("f7f07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("fbb07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("fdf07384-d9a0-4e6b-8a1d-4f4b8f8b8f8b"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "Name" },
                values: new object[,]
                {
                    { new Guid("0417e57b-03bd-41bf-ba6a-183faab813c8"), "Health" },
                    { new Guid("0fbc3c69-cae3-4747-82a3-c8f2bc841570"), "Charity" },
                    { new Guid("1169ae12-5a7e-48a7-a6d6-2a1a7e1c0be2"), "Photography" },
                    { new Guid("1ba48aab-06d1-4513-8d1a-9ca6e2150011"), "Finance" },
                    { new Guid("25f0cf63-26d8-440d-8f26-12177d935429"), "Social" },
                    { new Guid("2822802b-a6a2-4e01-a2d2-ed8dbb1b5f44"), "Community" },
                    { new Guid("37e0ac3a-02b7-4efc-9af3-5b57f5c2dd77"), "Dance" },
                    { new Guid("387756b4-019f-47ef-9faa-2ea83cf75773"), "Hobbies" },
                    { new Guid("3b4f39ef-b91a-4c21-94af-ffea46994078"), "Nature" },
                    { new Guid("5a1c8b11-fcaf-48ff-a8e2-60a22e3f49d7"), "Comedy" },
                    { new Guid("5dc8e075-02bc-40c0-89a8-2c5e0336ca2e"), "History" },
                    { new Guid("5f0dff34-416f-4441-9105-d5eaae2a7e3c"), "Politics" },
                    { new Guid("66962860-7fb2-43e8-8421-59e02e9e5868"), "Education" },
                    { new Guid("6a1ccd21-9719-45ea-9b20-ef1610802dd6"), "Networking" },
                    { new Guid("79e77c2d-7def-41f1-bcc0-5e8fda0a3169"), "Gaming" },
                    { new Guid("7d90f0a3-ab78-43ab-a8b2-1bac1994c9c9"), "Science" },
                    { new Guid("8b4c824a-41a6-443b-a144-1ee46ce20fc2"), "Fitness" },
                    { new Guid("8e13109e-b0c8-4984-b584-d7ed446920ac"), "Film" },
                    { new Guid("8eec4317-2bf3-409d-a6de-a02fb637288c"), "Travel" },
                    { new Guid("a12fd689-9a73-4f31-a63d-2deabd46f0ab"), "Art" },
                    { new Guid("b20962b2-8c64-4a0e-b44c-0f3859f293b9"), "Theatre" },
                    { new Guid("b2bcb1d8-6559-401d-a41d-4f5bfa60be2a"), "Literature" },
                    { new Guid("bbb80a03-e3c3-4782-9097-c0e806c3117c"), "Cooking" },
                    { new Guid("bc786c75-4c9b-4b62-a71a-3e0804231fc3"), "Lifestyle" },
                    { new Guid("d83d7fed-1665-416d-a912-69a4413f2b89"), "Business" },
                    { new Guid("e9b7be08-b33e-476a-88ee-cf088bea38a4"), "Music" },
                    { new Guid("ea5adb99-1fbb-41ff-b464-5d4ceb20960a"), "Environment" },
                    { new Guid("ee63f2cd-f9e8-49e2-a4ff-8c8eabcf0b59"), "Technology" },
                    { new Guid("f8265891-51e1-47ae-a846-f14bcdf42e8c"), "Sports" },
                    { new Guid("fde62827-21e1-4b3f-873a-cce9ac1c80aa"), "Writing" }
                });
        }
    }
}

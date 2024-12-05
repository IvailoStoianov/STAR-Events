using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace STAREvents.Data.Migrations
{
    /// <inheritdoc />
    public partial class Removedtags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("03257107-9495-4c69-9081-89a382f6bde1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("07a4450c-2574-436b-a6df-a520eaf674d1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("1845a30b-cce0-459d-918f-e6a0e5b9767b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("1c04da23-8db0-45bb-843f-b43593eb76f1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("2ab4139d-a9e2-4631-8a67-41badf1b6c85"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("2f0544fc-8738-4102-abdb-b5ce0e656531"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("32db7eb7-b92f-4314-b75a-3169042567c4"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("3e1c7689-bd40-4eab-a181-39e8388dd108"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("41b06f04-2c5f-42f3-8403-c94021a90e7a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("4e40f80e-051f-47b6-8dd9-72cc1687299c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("61653507-309a-4bd5-a9cb-b27aeec95de2"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("6be40c8d-61e3-4564-9950-f022a377a9c8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("77cb8e43-e3ad-466f-b553-180249c809d7"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("959a71a6-1048-435b-8d6b-6c690682a2b1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("b0cf550e-fade-470a-b37c-52a61435b4ce"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("b15d38b0-0ce1-469b-aded-5a87c8ce7055"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("b6f705e2-81ad-4330-9f04-d78e9dadf3bb"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("bb5fdb71-105b-494b-81b7-745cf2149469"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("be3981a0-87db-40dc-bfce-893ceabd143b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("bf2521f0-9c8f-413d-984e-3627176ff48d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("c0c3a194-5330-4611-9137-84d8f202952e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("c158e7ed-c772-444a-84df-2680f1097cd9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("c9b6867b-a9c7-4fda-810a-0d93d36f92ca"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("cde397cc-83df-4a23-bf4d-5a52806b5af0"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("d4e1036a-9046-4183-8ab6-0eb398401395"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("d8a0a89b-3ff7-4ef4-b635-4ea4beaf42f0"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("eecf5f73-ba38-4813-86ab-181730621401"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("f0095031-96a4-486a-a3aa-8a6821f2e9dc"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("fe91ab1f-d162-49df-8c0b-1f4fb8bbb73c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: new Guid("ff2931a2-6e87-420f-a991-9dfcb9c0b692"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                    table.ForeignKey(
                        name: "FK_Tags_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "Name" },
                values: new object[,]
                {
                    { new Guid("03257107-9495-4c69-9081-89a382f6bde1"), "Theatre" },
                    { new Guid("07a4450c-2574-436b-a6df-a520eaf674d1"), "Travel" },
                    { new Guid("1845a30b-cce0-459d-918f-e6a0e5b9767b"), "Networking" },
                    { new Guid("1c04da23-8db0-45bb-843f-b43593eb76f1"), "Community" },
                    { new Guid("2ab4139d-a9e2-4631-8a67-41badf1b6c85"), "Cooking" },
                    { new Guid("2f0544fc-8738-4102-abdb-b5ce0e656531"), "Science" },
                    { new Guid("32db7eb7-b92f-4314-b75a-3169042567c4"), "Fitness" },
                    { new Guid("3e1c7689-bd40-4eab-a181-39e8388dd108"), "Technology" },
                    { new Guid("41b06f04-2c5f-42f3-8403-c94021a90e7a"), "Comedy" },
                    { new Guid("4e40f80e-051f-47b6-8dd9-72cc1687299c"), "Nature" },
                    { new Guid("61653507-309a-4bd5-a9cb-b27aeec95de2"), "Gaming" },
                    { new Guid("6be40c8d-61e3-4564-9950-f022a377a9c8"), "Dance" },
                    { new Guid("77cb8e43-e3ad-466f-b553-180249c809d7"), "Finance" },
                    { new Guid("959a71a6-1048-435b-8d6b-6c690682a2b1"), "Art" },
                    { new Guid("b0cf550e-fade-470a-b37c-52a61435b4ce"), "Environment" },
                    { new Guid("b15d38b0-0ce1-469b-aded-5a87c8ce7055"), "Film" },
                    { new Guid("b6f705e2-81ad-4330-9f04-d78e9dadf3bb"), "Photography" },
                    { new Guid("bb5fdb71-105b-494b-81b7-745cf2149469"), "Music" },
                    { new Guid("be3981a0-87db-40dc-bfce-893ceabd143b"), "Literature" },
                    { new Guid("bf2521f0-9c8f-413d-984e-3627176ff48d"), "Health" },
                    { new Guid("c0c3a194-5330-4611-9137-84d8f202952e"), "Social" },
                    { new Guid("c158e7ed-c772-444a-84df-2680f1097cd9"), "Hobbies" },
                    { new Guid("c9b6867b-a9c7-4fda-810a-0d93d36f92ca"), "Politics" },
                    { new Guid("cde397cc-83df-4a23-bf4d-5a52806b5af0"), "History" },
                    { new Guid("d4e1036a-9046-4183-8ab6-0eb398401395"), "Sports" },
                    { new Guid("d8a0a89b-3ff7-4ef4-b635-4ea4beaf42f0"), "Charity" },
                    { new Guid("eecf5f73-ba38-4813-86ab-181730621401"), "Education" },
                    { new Guid("f0095031-96a4-486a-a3aa-8a6821f2e9dc"), "Business" },
                    { new Guid("fe91ab1f-d162-49df-8c0b-1f4fb8bbb73c"), "Lifestyle" },
                    { new Guid("ff2931a2-6e87-420f-a991-9dfcb9c0b692"), "Writing" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_EventId",
                table: "Tags",
                column: "EventId");
        }
    }
}

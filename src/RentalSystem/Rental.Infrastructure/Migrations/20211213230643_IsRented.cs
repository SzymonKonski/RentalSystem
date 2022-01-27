using Microsoft.EntityFrameworkCore.Migrations;

namespace Rental.Infrastructure.Migrations
{
    public partial class IsRented : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28f2d882-03fd-43ab-9d36-be4f63c4c5be");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f0ffd6e2-3a74-44e1-b616-5e5b64253bc1");

            migrationBuilder.AddColumn<bool>(
                name: "IsRented",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0d874b40-62b2-40b5-a0dc-e604180b74ae", "10e53e9c-07b7-4957-8ebd-dc6109691487", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "305ab074-91fb-4b9e-8f19-d36c6975359a", "c347e3f5-5c18-41f5-905f-98a7f2adff9f", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d874b40-62b2-40b5-a0dc-e604180b74ae");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "305ab074-91fb-4b9e-8f19-d36c6975359a");

            migrationBuilder.DropColumn(
                name: "IsRented",
                table: "Cars");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "28f2d882-03fd-43ab-9d36-be4f63c4c5be", "b27cf3f4-7c7f-45e4-b67e-84e9ee4de120", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f0ffd6e2-3a74-44e1-b616-5e5b64253bc1", "a6bf170d-5768-4312-b513-cd4231650716", "Administrator", "ADMINISTRATOR" });
        }
    }
}

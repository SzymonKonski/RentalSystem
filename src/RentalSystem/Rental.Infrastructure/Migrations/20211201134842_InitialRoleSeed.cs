using Microsoft.EntityFrameworkCore.Migrations;

namespace Rental.Infrastructure.Migrations
{
    public partial class InitialRoleSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d341a589-066a-40b8-91ed-995a32a12191", "8492e46f-6677-442f-ab27-aeb358b544c9", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "69d7a9ad-dc5b-4e16-ba5d-c81f393f4da4", "00bb8be4-2f98-40d6-b99d-90d0ceedc804", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69d7a9ad-dc5b-4e16-ba5d-c81f393f4da4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d341a589-066a-40b8-91ed-995a32a12191");
        }
    }
}

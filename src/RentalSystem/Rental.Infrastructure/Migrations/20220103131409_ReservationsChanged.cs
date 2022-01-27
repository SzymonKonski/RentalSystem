using Microsoft.EntityFrameworkCore.Migrations;

namespace Rental.Infrastructure.Migrations
{
    public partial class ReservationsChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReturnDate",
                table: "Rentals",
                newName: "RentTo");

            migrationBuilder.RenameColumn(
                name: "RentDate",
                table: "Rentals",
                newName: "RentFrom");

            migrationBuilder.AddColumn<bool>(
                name: "Returned",
                table: "Rentals",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Returned",
                table: "Rentals");

            migrationBuilder.RenameColumn(
                name: "RentTo",
                table: "Rentals",
                newName: "ReturnDate");

            migrationBuilder.RenameColumn(
                name: "RentFrom",
                table: "Rentals",
                newName: "RentDate");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Rent.DomainModels.Migrations
{
    public partial class AvailableCurrentlyChangedToCurrentlyRented : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableCurrently",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "CurrentlyRented",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentlyRented",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "AvailableCurrently",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

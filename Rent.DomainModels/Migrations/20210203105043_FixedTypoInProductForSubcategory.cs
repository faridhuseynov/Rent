using Microsoft.EntityFrameworkCore.Migrations;

namespace Rent.DomainModels.Migrations
{
    public partial class FixedTypoInProductForSubcategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Subcagetories_SubcagetoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubcagetoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubcagetoryId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "SubcategoryId",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubcategoryId",
                table: "Products",
                column: "SubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Subcagetories_SubcategoryId",
                table: "Products",
                column: "SubcategoryId",
                principalTable: "Subcagetories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Subcagetories_SubcategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubcategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubcategoryId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "SubcagetoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubcagetoryId",
                table: "Products",
                column: "SubcagetoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Subcagetories_SubcagetoryId",
                table: "Products",
                column: "SubcagetoryId",
                principalTable: "Subcagetories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

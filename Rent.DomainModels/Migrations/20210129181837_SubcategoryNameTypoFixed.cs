using Microsoft.EntityFrameworkCore.Migrations;

namespace Rent.DomainModels.Migrations
{
    public partial class SubcategoryNameTypoFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Subcagetories_SubcagetoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryDescription",
                table: "Subcagetories");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "Subcagetories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "SubcategoryDescription",
                table: "Subcagetories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubcategoryName",
                table: "Subcagetories",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubcagetoryId",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Subcagetories_SubcagetoryId",
                table: "Products",
                column: "SubcagetoryId",
                principalTable: "Subcagetories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Subcagetories_SubcagetoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubcategoryDescription",
                table: "Subcagetories");

            migrationBuilder.DropColumn(
                name: "SubcategoryName",
                table: "Subcagetories");

            migrationBuilder.AddColumn<string>(
                name: "CategoryDescription",
                table: "Subcagetories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "Subcagetories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubcagetoryId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Subcagetories_SubcagetoryId",
                table: "Products",
                column: "SubcagetoryId",
                principalTable: "Subcagetories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Rent.DomainModels.Migrations
{
    public partial class SubcategoryModelAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubcagetoryId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Subcagetories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: true),
                    CategoryDescription = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcagetories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcagetories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubcagetoryId",
                table: "Products",
                column: "SubcagetoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcagetories_CategoryId",
                table: "Subcagetories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Subcagetories_SubcagetoryId",
                table: "Products",
                column: "SubcagetoryId",
                principalTable: "Subcagetories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Subcagetories_SubcagetoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Subcagetories");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubcagetoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubcagetoryId",
                table: "Products");
        }
    }
}

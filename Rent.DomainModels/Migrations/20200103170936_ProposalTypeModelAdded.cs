using Microsoft.EntityFrameworkCore.Migrations;

namespace Rent.DomainModels.Migrations
{
    public partial class ProposalTypeModelAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProposalTypeId",
                table: "Proposals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProposalTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposalTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_ProposalTypeId",
                table: "Proposals",
                column: "ProposalTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_ProposalTypes_ProposalTypeId",
                table: "Proposals",
                column: "ProposalTypeId",
                principalTable: "ProposalTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_ProposalTypes_ProposalTypeId",
                table: "Proposals");

            migrationBuilder.DropTable(
                name: "ProposalTypes");

            migrationBuilder.DropIndex(
                name: "IX_Proposals_ProposalTypeId",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "ProposalTypeId",
                table: "Proposals");
        }
    }
}

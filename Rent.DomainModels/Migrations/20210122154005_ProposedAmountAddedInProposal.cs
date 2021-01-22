using Microsoft.EntityFrameworkCore.Migrations;

namespace Rent.DomainModels.Migrations
{
    public partial class ProposedAmountAddedInProposal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProposedAmount",
                table: "Proposals",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProposedAmount",
                table: "Proposals");
        }
    }
}

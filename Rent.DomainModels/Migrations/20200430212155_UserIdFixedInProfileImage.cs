using Microsoft.EntityFrameworkCore.Migrations;

namespace Rent.DomainModels.Migrations
{
    public partial class UserIdFixedInProfileImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileImages_AspNetUsers_UserId1",
                table: "ProfileImages");

            migrationBuilder.DropIndex(
                name: "IX_ProfileImages_UserId1",
                table: "ProfileImages");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ProfileImages");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ProfileImages",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileImages_UserId",
                table: "ProfileImages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileImages_AspNetUsers_UserId",
                table: "ProfileImages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileImages_AspNetUsers_UserId",
                table: "ProfileImages");

            migrationBuilder.DropIndex(
                name: "IX_ProfileImages_UserId",
                table: "ProfileImages");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ProfileImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "ProfileImages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileImages_UserId1",
                table: "ProfileImages",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileImages_AspNetUsers_UserId1",
                table: "ProfileImages",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

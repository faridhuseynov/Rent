﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Rent.DomainModels.Migrations
{
    public partial class UserAddedForRates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Rates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rates_UserId",
                table: "Rates",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_AspNetUsers_UserId",
                table: "Rates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_AspNetUsers_UserId",
                table: "Rates");

            migrationBuilder.DropIndex(
                name: "IX_Rates_UserId",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Rates");
        }
    }
}

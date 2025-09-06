using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_LoveCardOption_Add_OptionPlayerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OptionPlayerId",
                table: "LoveCardOptions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_LoveCardOptions_OptionPlayerId",
                table: "LoveCardOptions",
                column: "OptionPlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoveCardOptions_Players_OptionPlayerId",
                table: "LoveCardOptions",
                column: "OptionPlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoveCardOptions_Players_OptionPlayerId",
                table: "LoveCardOptions");

            migrationBuilder.DropIndex(
                name: "IX_LoveCardOptions_OptionPlayerId",
                table: "LoveCardOptions");

            migrationBuilder.DropColumn(
                name: "OptionPlayerId",
                table: "LoveCardOptions");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_PlayerLabel_For_Remove_PlayerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerLabels_Players_PlayerId",
                table: "PlayerLabels");

            migrationBuilder.DropIndex(
                name: "IX_PlayerLabels_PlayerId",
                table: "PlayerLabels");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "PlayerLabels");

            migrationBuilder.AddColumn<Guid>(
                name: "LoveCardId",
                table: "PlayerLabels",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerLabels_LoveCardId",
                table: "PlayerLabels",
                column: "LoveCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerLabels_LoveCards_LoveCardId",
                table: "PlayerLabels",
                column: "LoveCardId",
                principalTable: "LoveCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerLabels_LoveCards_LoveCardId",
                table: "PlayerLabels");

            migrationBuilder.DropIndex(
                name: "IX_PlayerLabels_LoveCardId",
                table: "PlayerLabels");

            migrationBuilder.DropColumn(
                name: "LoveCardId",
                table: "PlayerLabels");

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerId",
                table: "PlayerLabels",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PlayerLabels_PlayerId",
                table: "PlayerLabels",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerLabels_Players_PlayerId",
                table: "PlayerLabels",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

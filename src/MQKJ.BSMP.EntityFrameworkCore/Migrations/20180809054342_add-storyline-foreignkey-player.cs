using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addstorylineforeignkeyplayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoryLines_Players_PlayerId",
                table: "StoryLines");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryLines_Players_PlayerId1",
                table: "StoryLines");

            migrationBuilder.DropIndex(
                name: "IX_StoryLines_PlayerId",
                table: "StoryLines");

            migrationBuilder.DropIndex(
                name: "IX_StoryLines_PlayerId1",
                table: "StoryLines");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "StoryLines");

            migrationBuilder.DropColumn(
                name: "PlayerId1",
                table: "StoryLines");

            migrationBuilder.CreateIndex(
                name: "IX_StoryLines_PlayerAId",
                table: "StoryLines",
                column: "PlayerAId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryLines_PlayerBId",
                table: "StoryLines",
                column: "PlayerBId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoryLines_Players_PlayerAId",
                table: "StoryLines",
                column: "PlayerAId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryLines_Players_PlayerBId",
                table: "StoryLines",
                column: "PlayerBId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoryLines_Players_PlayerAId",
                table: "StoryLines");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryLines_Players_PlayerBId",
                table: "StoryLines");

            migrationBuilder.DropIndex(
                name: "IX_StoryLines_PlayerAId",
                table: "StoryLines");

            migrationBuilder.DropIndex(
                name: "IX_StoryLines_PlayerBId",
                table: "StoryLines");

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerId",
                table: "StoryLines",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerId1",
                table: "StoryLines",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoryLines_PlayerId",
                table: "StoryLines",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryLines_PlayerId1",
                table: "StoryLines",
                column: "PlayerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_StoryLines_Players_PlayerId",
                table: "StoryLines",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryLines_Players_PlayerId1",
                table: "StoryLines",
                column: "PlayerId1",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

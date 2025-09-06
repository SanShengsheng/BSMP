using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addquestionauthorandcheckOnefiled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckDateTime",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CheckOneId",
                table: "Questions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CheckOneId",
                table: "Questions",
                column: "CheckOneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_AbpUsers_CheckOneId",
                table: "Questions",
                column: "CheckOneId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_AbpUsers_CheckOneId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_CheckOneId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CheckDateTime",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CheckOneId",
                table: "Questions");
        }
    }
}

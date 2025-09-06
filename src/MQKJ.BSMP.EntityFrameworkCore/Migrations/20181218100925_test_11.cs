using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class test_11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerLabels_LoveCards_LoveCardId",
                table: "PlayerLabels");

            migrationBuilder.AlterColumn<Guid>(
                name: "LoveCardId",
                table: "PlayerLabels",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerLabels_LoveCards_LoveCardId",
                table: "PlayerLabels",
                column: "LoveCardId",
                principalTable: "LoveCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerLabels_LoveCards_LoveCardId",
                table: "PlayerLabels");

            migrationBuilder.AlterColumn<Guid>(
                name: "LoveCardId",
                table: "PlayerLabels",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerLabels_LoveCards_LoveCardId",
                table: "PlayerLabels",
                column: "LoveCardId",
                principalTable: "LoveCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

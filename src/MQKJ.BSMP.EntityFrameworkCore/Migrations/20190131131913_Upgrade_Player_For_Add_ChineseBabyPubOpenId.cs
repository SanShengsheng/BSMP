using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_Player_For_Add_ChineseBabyPubOpenId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Players_PlayerId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "ChineseBabyPubOpenId",
                table: "Players",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PlayerId",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Players_PlayerId",
                table: "Orders",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Players_PlayerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ChineseBabyPubOpenId",
                table: "Players");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlayerId",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Players_PlayerId",
                table: "Orders",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

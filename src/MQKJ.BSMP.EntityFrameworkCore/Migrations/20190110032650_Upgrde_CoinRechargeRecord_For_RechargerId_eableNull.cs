using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrde_CoinRechargeRecord_For_RechargerId_eableNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoinRechargeRecords_Players_RechargerId",
                table: "CoinRechargeRecords");

            migrationBuilder.AlterColumn<Guid>(
                name: "RechargerId",
                table: "CoinRechargeRecords",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_CoinRechargeRecords_Players_RechargerId",
                table: "CoinRechargeRecords",
                column: "RechargerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoinRechargeRecords_Players_RechargerId",
                table: "CoinRechargeRecords");

            migrationBuilder.AlterColumn<Guid>(
                name: "RechargerId",
                table: "CoinRechargeRecords",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CoinRechargeRecords_Players_RechargerId",
                table: "CoinRechargeRecords",
                column: "RechargerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

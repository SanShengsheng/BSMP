using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_CoinRechargeRecord_For_Add_CoinRechargeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoinRechargeId",
                table: "CoinRechargeRecords",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoinRechargeRecords_CoinRechargeId",
                table: "CoinRechargeRecords",
                column: "CoinRechargeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoinRechargeRecords_CoinRecharges_CoinRechargeId",
                table: "CoinRechargeRecords",
                column: "CoinRechargeId",
                principalTable: "CoinRecharges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoinRechargeRecords_CoinRecharges_CoinRechargeId",
                table: "CoinRechargeRecords");

            migrationBuilder.DropIndex(
                name: "IX_CoinRechargeRecords_CoinRechargeId",
                table: "CoinRechargeRecords");

            migrationBuilder.DropColumn(
                name: "CoinRechargeId",
                table: "CoinRechargeRecords");
        }
    }
}

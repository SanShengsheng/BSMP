using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_CoinRecharges_And_CoinRechargesRecord_For_Add_RechargeLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RechargeLevel",
                table: "CoinRecharges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RechargeLevel",
                table: "CoinRechargeRecords",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RechargeLevel",
                table: "CoinRecharges");

            migrationBuilder.DropColumn(
                name: "RechargeLevel",
                table: "CoinRechargeRecords");
        }
    }
}

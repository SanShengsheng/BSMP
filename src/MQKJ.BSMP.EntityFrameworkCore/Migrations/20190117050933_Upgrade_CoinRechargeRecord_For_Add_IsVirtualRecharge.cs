using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_CoinRechargeRecord_For_Add_IsVirtualRecharge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "VirtualRecharge",
                table: "Families",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsVirtualRecharge",
                table: "CoinRechargeRecords",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VirtualRecharge",
                table: "Families");

            migrationBuilder.DropColumn(
                name: "IsVirtualRecharge",
                table: "CoinRechargeRecords");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_EnterpirsePaymentRecord_For_Add_WithdrawMoneyType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WithdrawMoneyType",
                table: "EnterpirsePaymentRecords",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WithdrawMoneyType",
                table: "EnterpirsePaymentRecords");
        }
    }
}

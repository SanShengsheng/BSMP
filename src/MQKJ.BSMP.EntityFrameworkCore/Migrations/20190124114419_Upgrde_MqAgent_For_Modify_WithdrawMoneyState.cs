using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrde_MqAgent_For_Modify_WithdrawMoneyState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWithdrawMoney",
                table: "MqAgents");

            migrationBuilder.AddColumn<int>(
                name: "WithdrawMoneyState",
                table: "MqAgents",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WithdrawMoneyState",
                table: "MqAgents");

            migrationBuilder.AddColumn<bool>(
                name: "IsWithdrawMoney",
                table: "MqAgents",
                nullable: false,
                defaultValue: false);
        }
    }
}

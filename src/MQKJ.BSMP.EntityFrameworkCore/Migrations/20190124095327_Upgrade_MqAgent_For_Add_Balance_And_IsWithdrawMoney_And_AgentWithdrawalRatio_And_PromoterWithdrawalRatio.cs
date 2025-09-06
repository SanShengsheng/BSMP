using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_MqAgent_For_Add_Balance_And_IsWithdrawMoney_And_AgentWithdrawalRatio_And_PromoterWithdrawalRatio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WithdrawalRatio",
                table: "MqAgents",
                newName: "Balance");

            migrationBuilder.AddColumn<int>(
                name: "AgentWithdrawalRatio",
                table: "MqAgents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsWithdrawMoney",
                table: "MqAgents",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PromoterWithdrawalRatio",
                table: "MqAgents",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgentWithdrawalRatio",
                table: "MqAgents");

            migrationBuilder.DropColumn(
                name: "IsWithdrawMoney",
                table: "MqAgents");

            migrationBuilder.DropColumn(
                name: "PromoterWithdrawalRatio",
                table: "MqAgents");

            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "MqAgents",
                newName: "WithdrawalRatio");
        }
    }
}

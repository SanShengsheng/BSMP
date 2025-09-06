using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_EnterpirsePaymentRecord_For_MqAggent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnterpirsePaymentRecords_MqAgents_MqAgentId",
                table: "EnterpirsePaymentRecords");

            migrationBuilder.DropIndex(
                name: "IX_EnterpirsePaymentRecords_MqAgentId",
                table: "EnterpirsePaymentRecords");

            migrationBuilder.DropColumn(
                name: "MqAgentId",
                table: "EnterpirsePaymentRecords");

            migrationBuilder.CreateIndex(
                name: "IX_EnterpirsePaymentRecords_AgentId",
                table: "EnterpirsePaymentRecords",
                column: "AgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnterpirsePaymentRecords_MqAgents_AgentId",
                table: "EnterpirsePaymentRecords",
                column: "AgentId",
                principalTable: "MqAgents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnterpirsePaymentRecords_MqAgents_AgentId",
                table: "EnterpirsePaymentRecords");

            migrationBuilder.DropIndex(
                name: "IX_EnterpirsePaymentRecords_AgentId",
                table: "EnterpirsePaymentRecords");

            migrationBuilder.AddColumn<int>(
                name: "MqAgentId",
                table: "EnterpirsePaymentRecords",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnterpirsePaymentRecords_MqAgentId",
                table: "EnterpirsePaymentRecords",
                column: "MqAgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnterpirsePaymentRecords_MqAgents_MqAgentId",
                table: "EnterpirsePaymentRecords",
                column: "MqAgentId",
                principalTable: "MqAgents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

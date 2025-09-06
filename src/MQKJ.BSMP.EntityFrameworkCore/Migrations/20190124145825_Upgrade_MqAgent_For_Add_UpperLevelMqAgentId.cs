using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_MqAgent_For_Add_UpperLevelMqAgentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UpperLevelMqAgentId",
                table: "MqAgents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MqAgents_UpperLevelMqAgentId",
                table: "MqAgents",
                column: "UpperLevelMqAgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MqAgents_MqAgents_UpperLevelMqAgentId",
                table: "MqAgents",
                column: "UpperLevelMqAgentId",
                principalTable: "MqAgents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MqAgents_MqAgents_UpperLevelMqAgentId",
                table: "MqAgents");

            migrationBuilder.DropIndex(
                name: "IX_MqAgents_UpperLevelMqAgentId",
                table: "MqAgents");

            migrationBuilder.DropColumn(
                name: "UpperLevelMqAgentId",
                table: "MqAgents");
        }
    }
}

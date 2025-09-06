using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_MqAgent_For_Add_CompanyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "MqAgents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MqAgents_CompanyId",
                table: "MqAgents",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_MqAgents_Companies_CompanyId",
                table: "MqAgents",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MqAgents_Companies_CompanyId",
                table: "MqAgents");

            migrationBuilder.DropIndex(
                name: "IX_MqAgents_CompanyId",
                table: "MqAgents");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "MqAgents");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_IncomeRecord_For_Add_SecondAgentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<double>(
            //    name: "LockedBalance",
            //    table: "MqAgents",
            //    nullable: false,
            //    defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "SecondAgentId",
                table: "IncomeRecords",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IncomeRecords_SecondAgentId",
                table: "IncomeRecords",
                column: "SecondAgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeRecords_MqAgents_SecondAgentId",
                table: "IncomeRecords",
                column: "SecondAgentId",
                principalTable: "MqAgents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeRecords_MqAgents_SecondAgentId",
                table: "IncomeRecords");

            migrationBuilder.DropIndex(
                name: "IX_IncomeRecords_SecondAgentId",
                table: "IncomeRecords");

            //migrationBuilder.DropColumn(
            //    name: "LockedBalance",
            //    table: "MqAgents");

            migrationBuilder.DropColumn(
                name: "SecondAgentId",
                table: "IncomeRecords");
        }
    }
}

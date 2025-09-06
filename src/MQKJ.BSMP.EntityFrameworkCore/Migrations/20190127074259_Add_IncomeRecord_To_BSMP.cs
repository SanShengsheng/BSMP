using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Add_IncomeRecord_To_BSMP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IncomeRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Income = table.Column<double>(nullable: false),
                    RealIncome = table.Column<double>(nullable: false),
                    CurrentEarningRatio = table.Column<double>(nullable: false),
                    MqAgentId = table.Column<int>(nullable: false),
                    WithdrawMoneyState = table.Column<int>(nullable: false),
                    TaxRate = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncomeRecords_MqAgents_MqAgentId",
                        column: x => x.MqAgentId,
                        principalTable: "MqAgents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IncomeRecords_MqAgentId",
                table: "IncomeRecords",
                column: "MqAgentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncomeRecords");
        }
    }
}

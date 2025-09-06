using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Add_EnterpirsePaymentRecord_To_BSMP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<bool>(
            //    name: "IsCurrent",
            //    table: "PlayerProfessions",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<int>(
            //    name: "GoodsType",
            //    table: "Orders",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<string>(
            //    name: "PaymentData",
            //    table: "Orders",
            //    nullable: true);

            //migrationBuilder.AddColumn<double>(
            //    name: "SettlementTotalFee",
            //    table: "Orders",
            //    nullable: false,
            //    defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "EnterpirsePaymentRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    OutTradeNo = table.Column<string>(nullable: true),
                    AgentId = table.Column<int>(nullable: false),
                    MqAgentId = table.Column<int>(nullable: true),
                    State = table.Column<int>(nullable: false),
                    PaymentData = table.Column<string>(nullable: true),
                    PaymentNo = table.Column<string>(nullable: true),
                    PaymentTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterpirsePaymentRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnterpirsePaymentRecords_MqAgents_MqAgentId",
                        column: x => x.MqAgentId,
                        principalTable: "MqAgents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnterpirsePaymentRecords_MqAgentId",
                table: "EnterpirsePaymentRecords",
                column: "MqAgentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnterpirsePaymentRecords");

            //migrationBuilder.DropColumn(
            //    name: "IsCurrent",
            //    table: "PlayerProfessions");

            //migrationBuilder.DropColumn(
            //    name: "GoodsType",
            //    table: "Orders");

            //migrationBuilder.DropColumn(
            //    name: "PaymentData",
            //    table: "Orders");

            //migrationBuilder.DropColumn(
            //    name: "SettlementTotalFee",
            //    table: "Orders");
        }
    }
}

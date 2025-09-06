using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_IncomeRecord_For_Add_Foreign_OrderId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_IncomeRecords_OrderId",
                table: "IncomeRecords",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeRecords_Orders_OrderId",
                table: "IncomeRecords",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeRecords_Orders_OrderId",
                table: "IncomeRecords");

            migrationBuilder.DropIndex(
                name: "IX_IncomeRecords_OrderId",
                table: "IncomeRecords");
        }
    }
}

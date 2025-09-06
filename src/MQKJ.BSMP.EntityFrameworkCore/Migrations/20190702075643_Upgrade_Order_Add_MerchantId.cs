using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_Order_Add_MerchantId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "WechatMerchants",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "CompanyId",
            //    table: "Orders",
            //    nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WechatMerchantId",
                table: "Orders",
                nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "CompanyId",
            //    table: "IncomeRecords",
            //    nullable: true);

            //migrationBuilder.AddColumn<double>(
            //    name: "RoyaltyRate",
            //    table: "Companies",
            //    nullable: false,
            //    defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WechatMerchantId",
                table: "Orders",
                column: "WechatMerchantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_WechatMerchants_WechatMerchantId",
                table: "Orders",
                column: "WechatMerchantId",
                principalTable: "WechatMerchants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_WechatMerchants_WechatMerchantId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_WechatMerchantId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "WechatMerchants");

            //migrationBuilder.DropColumn(
            //    name: "CompanyId",
            //    table: "Orders");

            migrationBuilder.DropColumn(
                name: "WechatMerchantId",
                table: "Orders");

            //migrationBuilder.DropColumn(
            //    name: "CompanyId",
            //    table: "IncomeRecords");

            //migrationBuilder.DropColumn(
            //    name: "RoyaltyRate",
            //    table: "Companies");
        }
    }
}

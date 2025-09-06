using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addcardNofieldrequestPlatform_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardNo",
                table: "MqAgents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestPlatform",
                table: "EnterpirsePaymentRecords",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardNo",
                table: "MqAgents");

            migrationBuilder.DropColumn(
                name: "RequestPlatform",
                table: "EnterpirsePaymentRecords");
        }
    }
}

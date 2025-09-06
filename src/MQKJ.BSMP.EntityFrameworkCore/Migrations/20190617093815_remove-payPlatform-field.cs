using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class removepayPlatformfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayPlatform",
                table: "EnterpirsePaymentRecords");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PayPlatform",
                table: "EnterpirsePaymentRecords",
                nullable: false,
                defaultValue: 0);
        }
    }
}

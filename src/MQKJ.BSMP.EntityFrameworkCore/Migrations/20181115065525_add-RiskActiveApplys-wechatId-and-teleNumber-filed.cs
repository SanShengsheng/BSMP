using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addRiskActiveApplyswechatIdandteleNumberfiled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeleNumber",
                table: "RiskActiveApplys",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WechatId",
                table: "RiskActiveApplys",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeleNumber",
                table: "RiskActiveApplys");

            migrationBuilder.DropColumn(
                name: "WechatId",
                table: "RiskActiveApplys");
        }
    }
}

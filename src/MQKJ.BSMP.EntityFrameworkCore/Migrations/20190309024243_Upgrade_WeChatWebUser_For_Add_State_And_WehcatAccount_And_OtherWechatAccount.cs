using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_WeChatWebUser_For_Add_State_And_WehcatAccount_And_OtherWechatAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OtherWechatAccount",
                table: "WeChatWebUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "WeChatWebUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WechatAccount",
                table: "WeChatWebUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherWechatAccount",
                table: "WeChatWebUsers");

            migrationBuilder.DropColumn(
                name: "State",
                table: "WeChatWebUsers");

            migrationBuilder.DropColumn(
                name: "WechatAccount",
                table: "WeChatWebUsers");
        }
    }
}

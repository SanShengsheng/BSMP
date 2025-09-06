using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_WeChatWebUser_For_Add_Profession_And_Interest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Country",
                table: "WeChatWebUsers",
                newName: "Profession");

            migrationBuilder.AddColumn<string>(
                name: "Interest",
                table: "WeChatWebUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interest",
                table: "WeChatWebUsers");

            migrationBuilder.RenameColumn(
                name: "Profession",
                table: "WeChatWebUsers",
                newName: "Country");
        }
    }
}

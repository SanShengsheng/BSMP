using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgarde_MqAgent_Add_NickName_And_HeadUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HeadUrl",
                table: "MqAgents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NickName",
                table: "MqAgents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeadUrl",
                table: "MqAgents");

            migrationBuilder.DropColumn(
                name: "NickName",
                table: "MqAgents");
        }
    }
}

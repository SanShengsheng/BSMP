using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_MqAgent_For_Add_UserName_PhoneNumber_IdCardNumber_GroupId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAgenter",
                table: "Players",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "GroupId",
                table: "MqAgents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdCardNumber",
                table: "MqAgents",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "MqAgents",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "MqAgents",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAgenter",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "MqAgents");

            migrationBuilder.DropColumn(
                name: "IdCardNumber",
                table: "MqAgents");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "MqAgents");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "MqAgents");
        }
    }
}

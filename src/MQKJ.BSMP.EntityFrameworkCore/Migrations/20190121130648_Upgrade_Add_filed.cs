using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_Add_filed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OpenId",
                table: "MqAgents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddOnNote",
                table: "Families",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddOnStatus",
                table: "Families",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BuyCount",
                table: "AutoRunnerConfigs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LevelAction",
                table: "AutoRunnerConfigs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudyLevel",
                table: "AutoRunnerConfigs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpenId",
                table: "MqAgents");

            migrationBuilder.DropColumn(
                name: "AddOnNote",
                table: "Families");

            migrationBuilder.DropColumn(
                name: "AddOnStatus",
                table: "Families");

            migrationBuilder.DropColumn(
                name: "BuyCount",
                table: "AutoRunnerConfigs");

            migrationBuilder.DropColumn(
                name: "LevelAction",
                table: "AutoRunnerConfigs");

            migrationBuilder.DropColumn(
                name: "StudyLevel",
                table: "AutoRunnerConfigs");
        }
    }
}

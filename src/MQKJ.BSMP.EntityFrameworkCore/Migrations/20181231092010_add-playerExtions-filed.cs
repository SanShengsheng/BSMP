using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addplayerExtionsfiled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "PlayerExtensions",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "PlayerExtensions",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "PlayerExtensions",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "PlayerExtensions");

            migrationBuilder.DropColumn(
                name: "Profession",
                table: "PlayerExtensions");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "PlayerExtensions");
        }
    }
}

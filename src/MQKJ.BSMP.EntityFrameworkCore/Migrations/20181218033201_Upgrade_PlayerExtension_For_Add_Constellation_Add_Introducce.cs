using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_PlayerExtension_For_Add_Constellation_Add_Introducce : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Constellation",
                table: "PlayerExtensions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Introduce",
                table: "PlayerExtensions",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Constellation",
                table: "PlayerExtensions");

            migrationBuilder.DropColumn(
                name: "Introduce",
                table: "PlayerExtensions");
        }
    }
}

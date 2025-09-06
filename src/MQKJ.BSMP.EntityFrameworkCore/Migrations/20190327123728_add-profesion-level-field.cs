using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addprofesionlevelfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "PlayerProfessions");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Professions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Professions");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "PlayerProfessions",
                nullable: false,
                defaultValue: 0);
        }
    }
}

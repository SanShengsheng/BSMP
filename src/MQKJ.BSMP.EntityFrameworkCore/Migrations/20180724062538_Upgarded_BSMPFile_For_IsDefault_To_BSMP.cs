using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgarded_BSMPFile_For_IsDefault_To_BSMP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "BSMPFiles");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "SceneFiles",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "SceneFiles");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "BSMPFiles",
                nullable: false,
                defaultValue: false);
        }
    }
}

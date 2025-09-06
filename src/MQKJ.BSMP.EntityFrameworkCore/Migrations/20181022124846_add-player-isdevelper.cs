using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addplayerisdevelper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWaiter",
                table: "PlayerExtensions");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeveloper",
                table: "Players",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeveloper",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "IsWaiter",
                table: "PlayerExtensions",
                nullable: false,
                defaultValue: 0);
        }
    }
}

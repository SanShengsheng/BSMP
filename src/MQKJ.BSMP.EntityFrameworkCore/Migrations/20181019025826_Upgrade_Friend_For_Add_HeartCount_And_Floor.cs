using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_Friend_For_Add_HeartCount_And_Floor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "Friends",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "HeartCount",
                table: "Friends",
                nullable: false,
                defaultValue: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Floor",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "HeartCount",
                table: "Friends");
        }
    }
}

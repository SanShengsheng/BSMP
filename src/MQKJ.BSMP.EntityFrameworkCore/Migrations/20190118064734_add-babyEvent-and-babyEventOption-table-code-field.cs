using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addbabyEventandbabyEventOptiontablecodefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "EventGroups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "BabyEvents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "BabyEventOptions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "EventGroups");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "BabyEvents");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "BabyEventOptions");
        }
    }
}

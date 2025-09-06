using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addmaxandminpropertybabyEnding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxProperty",
                table: "BabyEndings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinProperty",
                table: "BabyEndings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxProperty",
                table: "BabyEndings");

            migrationBuilder.DropColumn(
                name: "MinProperty",
                table: "BabyEndings");
        }
    }
}

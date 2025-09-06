using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_Informations_For_Add_State : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Informations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Informations");
        }
    }
}

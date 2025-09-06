using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class updatebabyfiledchildeBirthOrdertobirthOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChildBirthOrder",
                table: "Babies",
                newName: "BirthOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthOrder",
                table: "Babies",
                newName: "ChildBirthOrder");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addlookEndingfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PropertyTotal",
                table: "BabyEndings",
                newName: "PropertyTotalMin");

            migrationBuilder.AddColumn<int>(
                name: "PropertyTotalMax",
                table: "BabyEndings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsLookEndingFather",
                table: "Babies",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLookEndingMother",
                table: "Babies",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PropertyTotalMax",
                table: "BabyEndings");

            migrationBuilder.DropColumn(
                name: "IsLookEndingFather",
                table: "Babies");

            migrationBuilder.DropColumn(
                name: "IsLookEndingMother",
                table: "Babies");

            migrationBuilder.RenameColumn(
                name: "PropertyTotalMin",
                table: "BabyEndings",
                newName: "PropertyTotal");
        }
    }
}

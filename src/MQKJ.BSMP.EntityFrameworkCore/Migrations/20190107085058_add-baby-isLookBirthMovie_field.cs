using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addbabyisLookBirthMovie_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLoadBirthMovieMother",
                table: "Babies",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsWatchBirthMovieFather",
                table: "Babies",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLoadBirthMovieMother",
                table: "Babies");

            migrationBuilder.DropColumn(
                name: "IsWatchBirthMovieFather",
                table: "Babies");
        }
    }
}

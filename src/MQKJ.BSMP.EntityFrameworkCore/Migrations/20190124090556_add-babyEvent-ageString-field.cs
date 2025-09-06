using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addbabyEventageStringfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Age",
                table: "BabyEvents",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AgeString",
                table: "BabyEvents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeString",
                table: "BabyEvents");

            migrationBuilder.AlterColumn<string>(
                name: "Age",
                table: "BabyEvents",
                nullable: true,
                oldClrType: typeof(double));
        }
    }
}

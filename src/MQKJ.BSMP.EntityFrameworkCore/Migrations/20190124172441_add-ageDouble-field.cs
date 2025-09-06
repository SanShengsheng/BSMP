using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addageDoublefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Age",
                table: "BabyEvents",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<double>(
                name: "AgeDouble",
                table: "Babies",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeDouble",
                table: "Babies");

            migrationBuilder.AlterColumn<double>(
                name: "Age",
                table: "BabyEvents",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addbabyEventgameTimefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StudyType",
                table: "BabyEvents",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "StudyAllowMaxTime",
                table: "BabyEvents",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "GameTime",
                table: "BabyEvents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameTime",
                table: "BabyEvents");

            migrationBuilder.AlterColumn<int>(
                name: "StudyType",
                table: "BabyEvents",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudyAllowMaxTime",
                table: "BabyEvents",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}

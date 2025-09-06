using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class resetanswertiltelengthto72 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Answers",
                maxLength: 72,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 34);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Answers",
                maxLength: 34,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 72);
        }
    }
}

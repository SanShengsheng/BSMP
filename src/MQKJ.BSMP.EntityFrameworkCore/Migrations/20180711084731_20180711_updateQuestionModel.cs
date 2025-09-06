using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class _20180711_updateQuestionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "State",
                table: "Questions",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AnswerCount",
                table: "Questions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sorting",
                table: "Questions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Questions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerCount",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Sorting",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Questions");

            migrationBuilder.AlterColumn<int>(
                name: "State",
                table: "Questions",
                nullable: false,
                oldClrType: typeof(byte));
        }
    }
}

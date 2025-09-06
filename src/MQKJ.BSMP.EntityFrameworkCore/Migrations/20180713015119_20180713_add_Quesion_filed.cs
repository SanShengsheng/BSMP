using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class _20180713_add_Quesion_filed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuestionFemale",
                table: "Questions",
                maxLength: 72,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QuestionMale",
                table: "Questions",
                maxLength: 72,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionFemale",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuestionMale",
                table: "Questions");
        }
    }
}

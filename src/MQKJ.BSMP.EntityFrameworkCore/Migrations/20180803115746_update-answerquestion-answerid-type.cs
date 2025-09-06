using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class updateanswerquestionansweridtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InviteeAnswer",
                table: "AnswerQuestions");

            migrationBuilder.DropColumn(
                name: "InviterAnswer",
                table: "AnswerQuestions");

            migrationBuilder.AddColumn<int>(
                name: "InviteeAnswerId",
                table: "AnswerQuestions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InviterAnswerId",
                table: "AnswerQuestions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InviteeAnswerId",
                table: "AnswerQuestions");

            migrationBuilder.DropColumn(
                name: "InviterAnswerId",
                table: "AnswerQuestions");

            migrationBuilder.AddColumn<string>(
                name: "InviteeAnswer",
                table: "AnswerQuestions",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InviterAnswer",
                table: "AnswerQuestions",
                maxLength: 300,
                nullable: true);
        }
    }
}

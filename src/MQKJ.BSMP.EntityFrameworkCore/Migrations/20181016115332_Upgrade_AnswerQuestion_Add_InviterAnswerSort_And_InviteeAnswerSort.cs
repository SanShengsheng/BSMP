using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_AnswerQuestion_Add_InviterAnswerSort_And_InviteeAnswerSort : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sort",
                table: "AnswerQuestions",
                newName: "InviterAnswerSort");

            migrationBuilder.AddColumn<int>(
                name: "InviteeAnswerSort",
                table: "AnswerQuestions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InviteeAnswerSort",
                table: "AnswerQuestions");

            migrationBuilder.RenameColumn(
                name: "InviterAnswerSort",
                table: "AnswerQuestions",
                newName: "Sort");
        }
    }
}

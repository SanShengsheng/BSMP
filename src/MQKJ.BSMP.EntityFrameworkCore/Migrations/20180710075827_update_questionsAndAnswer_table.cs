using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class update_questionsAndAnswer_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionID",
                table: "Answers",
                column: "QuestionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionID",
                table: "Answers",
                column: "QuestionID",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionID",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionID",
                table: "Answers");
        }
    }
}

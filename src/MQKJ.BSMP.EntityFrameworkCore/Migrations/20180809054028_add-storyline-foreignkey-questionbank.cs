using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addstorylineforeignkeyquestionbank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StoryLines_QuestionBankId",
                table: "StoryLines",
                column: "QuestionBankId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoryLines_QuestionBanks_QuestionBankId",
                table: "StoryLines",
                column: "QuestionBankId",
                principalTable: "QuestionBanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoryLines_QuestionBanks_QuestionBankId",
                table: "StoryLines");

            migrationBuilder.DropIndex(
                name: "IX_StoryLines_QuestionBankId",
                table: "StoryLines");
        }
    }
}

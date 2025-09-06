using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addquestionBanksomefiled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "QuestionBanks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComplexityId",
                table: "QuestionBanks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SceneId",
                table: "QuestionBanks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SecretId",
                table: "QuestionBanks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "QuestionBanks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "QuestionBanks");

            migrationBuilder.DropColumn(
                name: "ComplexityId",
                table: "QuestionBanks");

            migrationBuilder.DropColumn(
                name: "SceneId",
                table: "QuestionBanks");

            migrationBuilder.DropColumn(
                name: "SecretId",
                table: "QuestionBanks");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "QuestionBanks");
        }
    }
}

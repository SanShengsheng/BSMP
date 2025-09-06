using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class somedatabaseupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TopicId",
                table: "QuestionBanks",
                newName: "RepeatTopicCount");

            migrationBuilder.RenameColumn(
                name: "SecretId",
                table: "QuestionBanks",
                newName: "RepeatSecretCount");

            migrationBuilder.RenameColumn(
                name: "SceneId",
                table: "QuestionBanks",
                newName: "RepeatSceneCount");

            migrationBuilder.RenameColumn(
                name: "ComplexityId",
                table: "QuestionBanks",
                newName: "RepeatComplexityCount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RepeatTopicCount",
                table: "QuestionBanks",
                newName: "TopicId");

            migrationBuilder.RenameColumn(
                name: "RepeatSecretCount",
                table: "QuestionBanks",
                newName: "SecretId");

            migrationBuilder.RenameColumn(
                name: "RepeatSceneCount",
                table: "QuestionBanks",
                newName: "SceneId");

            migrationBuilder.RenameColumn(
                name: "RepeatComplexityCount",
                table: "QuestionBanks",
                newName: "ComplexityId");
        }
    }
}

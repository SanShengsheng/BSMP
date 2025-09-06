using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addsomefieldending : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxHealthy",
                table: "BabyEndings",
                newName: "StudyMin");

            migrationBuilder.RenameColumn(
                name: "MaxEnergy",
                table: "BabyEndings",
                newName: "StudyMax");

            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "BabyEndings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "BabyEndings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinCharm",
                table: "BabyEndings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinEmotionQuotient",
                table: "BabyEndings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinImagine",
                table: "BabyEndings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinIntelligence",
                table: "BabyEndings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinPhysique",
                table: "BabyEndings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinWillPower",
                table: "BabyEndings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "BabyEndings");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "BabyEndings");

            migrationBuilder.DropColumn(
                name: "MinCharm",
                table: "BabyEndings");

            migrationBuilder.DropColumn(
                name: "MinEmotionQuotient",
                table: "BabyEndings");

            migrationBuilder.DropColumn(
                name: "MinImagine",
                table: "BabyEndings");

            migrationBuilder.DropColumn(
                name: "MinIntelligence",
                table: "BabyEndings");

            migrationBuilder.DropColumn(
                name: "MinPhysique",
                table: "BabyEndings");

            migrationBuilder.DropColumn(
                name: "MinWillPower",
                table: "BabyEndings");

            migrationBuilder.RenameColumn(
                name: "StudyMin",
                table: "BabyEndings",
                newName: "MaxHealthy");

            migrationBuilder.RenameColumn(
                name: "StudyMax",
                table: "BabyEndings",
                newName: "MaxEnergy");
        }
    }
}

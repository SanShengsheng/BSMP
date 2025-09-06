using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class removesomefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Charm",
                table: "BabyEndings");

            migrationBuilder.DropColumn(
                name: "EmotionQuotient",
                table: "BabyEndings");

            migrationBuilder.DropColumn(
                name: "Energy",
                table: "BabyEndings");

            migrationBuilder.DropColumn(
                name: "Healthy",
                table: "BabyEndings");

            migrationBuilder.DropColumn(
                name: "Imagine",
                table: "BabyEndings");

            migrationBuilder.DropColumn(
                name: "Intelligence",
                table: "BabyEndings");

            migrationBuilder.DropColumn(
                name: "Physique",
                table: "BabyEndings");

            migrationBuilder.DropColumn(
                name: "PropertyTotalMax",
                table: "BabyEndings");

            migrationBuilder.DropColumn(
                name: "PropertyTotalMin",
                table: "BabyEndings");

            migrationBuilder.DropColumn(
                name: "WillPower",
                table: "BabyEndings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Charm",
                table: "BabyEndings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmotionQuotient",
                table: "BabyEndings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Energy",
                table: "BabyEndings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Healthy",
                table: "BabyEndings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Imagine",
                table: "BabyEndings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Intelligence",
                table: "BabyEndings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Physique",
                table: "BabyEndings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PropertyTotalMax",
                table: "BabyEndings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PropertyTotalMin",
                table: "BabyEndings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WillPower",
                table: "BabyEndings",
                nullable: false,
                defaultValue: 0);
        }
    }
}

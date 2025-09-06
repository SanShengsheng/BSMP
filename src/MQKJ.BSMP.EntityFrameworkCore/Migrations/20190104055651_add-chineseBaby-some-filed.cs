using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addchineseBabysomefiled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Professions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "SatisfactionDegree",
                table: "Professions",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "BirthHospital",
                table: "Babies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BirthLength",
                table: "Babies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BirthWeight",
                table: "Babies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "GrowthTotal",
                table: "Babies",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Potential",
                table: "Babies",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Professions");

            migrationBuilder.DropColumn(
                name: "SatisfactionDegree",
                table: "Professions");

            migrationBuilder.DropColumn(
                name: "BirthHospital",
                table: "Babies");

            migrationBuilder.DropColumn(
                name: "BirthLength",
                table: "Babies");

            migrationBuilder.DropColumn(
                name: "BirthWeight",
                table: "Babies");

            migrationBuilder.DropColumn(
                name: "GrowthTotal",
                table: "Babies");

            migrationBuilder.DropColumn(
                name: "Potential",
                table: "Babies");
        }
    }
}

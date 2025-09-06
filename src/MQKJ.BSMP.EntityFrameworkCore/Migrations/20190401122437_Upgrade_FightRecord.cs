using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_FightRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FamilyId",
                table: "FightRecords");

            migrationBuilder.DropColumn(
                name: "WinnerId",
                table: "FightRecords");

            migrationBuilder.DropColumn(
                name: "failederId",
                table: "FightRecords");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "AttributeRate",
                table: "FightRecords",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttributeRate",
                table: "FightRecords");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "FamilyId",
                table: "FightRecords",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WinnerId",
                table: "FightRecords",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "failederId",
                table: "FightRecords",
                nullable: true);
        }
    }
}

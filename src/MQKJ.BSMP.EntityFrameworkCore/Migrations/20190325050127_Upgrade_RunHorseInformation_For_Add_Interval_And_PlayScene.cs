using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_RunHorseInformation_For_Add_Interval_And_PlayScene : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "RunHorseInformations",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<int>(
                name: "PlayCount",
                table: "RunHorseInformations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlayScene",
                table: "RunHorseInformations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayCount",
                table: "RunHorseInformations");

            migrationBuilder.DropColumn(
                name: "PlayScene",
                table: "RunHorseInformations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "RunHorseInformations",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_RunHorseInformations_For_Add_Priority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PlayCount",
                table: "RunHorseInformations",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Interval",
                table: "RunHorseInformations",
                nullable: false,
                defaultValue: 3,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(2119, 3, 25, 17, 4, 55, 599, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "RunHorseInformations",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "RunHorseInformations");

            migrationBuilder.AlterColumn<int>(
                name: "PlayCount",
                table: "RunHorseInformations",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Interval",
                table: "RunHorseInformations",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 3);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2119, 3, 25, 17, 4, 55, 599, DateTimeKind.Local));
        }
    }
}

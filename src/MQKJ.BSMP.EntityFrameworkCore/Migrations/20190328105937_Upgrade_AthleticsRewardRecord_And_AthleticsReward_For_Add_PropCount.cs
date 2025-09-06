using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_AthleticsRewardRecord_And_AthleticsReward_For_Add_PropCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2119, 3, 28, 17, 25, 6, 538, DateTimeKind.Local));

            migrationBuilder.AddColumn<int>(
                name: "BabyPropCount",
                table: "AthleticsRewards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PropCount",
                table: "AthleticsRewardRecords",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BabyPropCount",
                table: "AthleticsRewards");

            migrationBuilder.DropColumn(
                name: "PropCount",
                table: "AthleticsRewardRecords");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(2119, 3, 28, 17, 25, 6, 538, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addRewardPropTypefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(2119, 3, 28, 17, 25, 6, 538, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2119, 3, 26, 16, 14, 52, 572, DateTimeKind.Local));

            migrationBuilder.AddColumn<int>(
                name: "RewardPropType",
                table: "AthleticsRewards",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RewardPropType",
                table: "AthleticsRewards");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(2119, 3, 26, 16, 14, 52, 572, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2119, 3, 28, 17, 25, 6, 538, DateTimeKind.Local));
        }
    }
}

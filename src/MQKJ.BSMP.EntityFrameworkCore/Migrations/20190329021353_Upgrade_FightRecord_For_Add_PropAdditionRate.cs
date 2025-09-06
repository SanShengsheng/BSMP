using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_FightRecord_For_Add_PropAdditionRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BabyPropId",
                table: "AthleticsRewardRecords");

            migrationBuilder.DropColumn(
                name: "BabyPropPriceId",
                table: "AthleticsRewardRecords");

            migrationBuilder.DropColumn(
                name: "CoinCount",
                table: "AthleticsRewardRecords");

            migrationBuilder.RenameColumn(
                name: "PropCount",
                table: "AthleticsRewardRecords",
                newName: "AthleticsRewardId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "PropAdditionRate",
                table: "FightRecords",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_AthleticsRewardRecords_AthleticsRewardId",
                table: "AthleticsRewardRecords",
                column: "AthleticsRewardId");

            migrationBuilder.AddForeignKey(
                name: "FK_AthleticsRewardRecords_AthleticsRewards_AthleticsRewardId",
                table: "AthleticsRewardRecords",
                column: "AthleticsRewardId",
                principalTable: "AthleticsRewards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AthleticsRewardRecords_AthleticsRewards_AthleticsRewardId",
                table: "AthleticsRewardRecords");

            migrationBuilder.DropIndex(
                name: "IX_AthleticsRewardRecords_AthleticsRewardId",
                table: "AthleticsRewardRecords");

            migrationBuilder.DropColumn(
                name: "PropAdditionRate",
                table: "FightRecords");

            migrationBuilder.RenameColumn(
                name: "AthleticsRewardId",
                table: "AthleticsRewardRecords",
                newName: "PropCount");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "BabyPropId",
                table: "AthleticsRewardRecords",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BabyPropPriceId",
                table: "AthleticsRewardRecords",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoinCount",
                table: "AthleticsRewardRecords",
                nullable: false,
                defaultValue: 0);
        }
    }
}

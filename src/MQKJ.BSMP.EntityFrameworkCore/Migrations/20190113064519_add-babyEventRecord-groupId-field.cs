using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addbabyEventRecordgroupIdfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyEventRecords_BabyEventOptions_OptionId",
                table: "BabyEventRecords");

            migrationBuilder.AddColumn<int>(
                name: "PlayerGuid",
                schema: "dbo",
                table: "BabyGrowUpRecords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Happiness",
                table: "Rewards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginTime",
                table: "Players",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "OptionId",
                table: "BabyEventRecords",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "BabyId",
                table: "BabyEventRecords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FatherOptionId",
                table: "BabyEventRecords",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "BabyEventRecords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MotherOptionId",
                table: "BabyEventRecords",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BabyEventRecords_BabyEventOptions_OptionId",
                table: "BabyEventRecords",
                column: "OptionId",
                principalTable: "BabyEventOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyEventRecords_BabyEventOptions_OptionId",
                table: "BabyEventRecords");

            migrationBuilder.DropColumn(
                name: "PlayerGuid",
                schema: "dbo",
                table: "BabyGrowUpRecords");

            migrationBuilder.DropColumn(
                name: "Happiness",
                table: "Rewards");

            migrationBuilder.DropColumn(
                name: "LastLoginTime",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "BabyId",
                table: "BabyEventRecords");

            migrationBuilder.DropColumn(
                name: "FatherOptionId",
                table: "BabyEventRecords");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "BabyEventRecords");

            migrationBuilder.DropColumn(
                name: "MotherOptionId",
                table: "BabyEventRecords");

            migrationBuilder.AlterColumn<int>(
                name: "OptionId",
                table: "BabyEventRecords",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BabyEventRecords_BabyEventOptions_OptionId",
                table: "BabyEventRecords",
                column: "OptionId",
                principalTable: "BabyEventOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

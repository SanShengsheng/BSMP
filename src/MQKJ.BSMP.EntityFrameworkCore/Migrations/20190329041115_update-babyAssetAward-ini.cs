using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class updatebabyAssetAwardini : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(2119, 3, 29, 12, 11, 13, 434, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2119, 3, 28, 17, 25, 6, 538, DateTimeKind.Local));

            migrationBuilder.AddColumn<int>(
                name: "BabyId",
                table: "BabyAssetAwards",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Charm",
                table: "BabyAssetAwards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmotionQuotient",
                table: "BabyAssetAwards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Imagine",
                table: "BabyAssetAwards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Intelligence",
                table: "BabyAssetAwards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Physique",
                table: "BabyAssetAwards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "BabyAssetAwards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WillPower",
                table: "BabyAssetAwards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BabyAssetAwards_BabyId",
                table: "BabyAssetAwards",
                column: "BabyId");

            migrationBuilder.AddForeignKey(
                name: "FK_BabyAssetAwards_Babies_BabyId",
                table: "BabyAssetAwards",
                column: "BabyId",
                principalTable: "Babies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyAssetAwards_Babies_BabyId",
                table: "BabyAssetAwards");

            migrationBuilder.DropIndex(
                name: "IX_BabyAssetAwards_BabyId",
                table: "BabyAssetAwards");

            migrationBuilder.DropColumn(
                name: "BabyId",
                table: "BabyAssetAwards");

            migrationBuilder.DropColumn(
                name: "Charm",
                table: "BabyAssetAwards");

            migrationBuilder.DropColumn(
                name: "EmotionQuotient",
                table: "BabyAssetAwards");

            migrationBuilder.DropColumn(
                name: "Imagine",
                table: "BabyAssetAwards");

            migrationBuilder.DropColumn(
                name: "Intelligence",
                table: "BabyAssetAwards");

            migrationBuilder.DropColumn(
                name: "Physique",
                table: "BabyAssetAwards");

            migrationBuilder.DropColumn(
                name: "State",
                table: "BabyAssetAwards");

            migrationBuilder.DropColumn(
                name: "WillPower",
                table: "BabyAssetAwards");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(2119, 3, 28, 17, 25, 6, 538, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2119, 3, 29, 12, 11, 13, 434, DateTimeKind.Local));
        }
    }
}

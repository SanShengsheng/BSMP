using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addbabyPropBagsomefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyPropRecords_BabyProps_BabyPropId",
                table: "BabyPropRecords");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "BabyPropId",
                table: "BabyPropRecords",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "BabyPropBagId",
                table: "BabyPropRecords",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BabyPropBagId1",
                table: "BabyPropRecords",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "BabyPropBags",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PriceGoldCoin",
                table: "BabyPropBags",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceRMB",
                table: "BabyPropBags",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "BabyPropBags",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDisabled",
                table: "BabyPropBags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_BabyPropRecords_BabyPropBagId1",
                table: "BabyPropRecords",
                column: "BabyPropBagId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BabyPropRecords_BabyPropBags_BabyPropBagId1",
                table: "BabyPropRecords",
                column: "BabyPropBagId1",
                principalTable: "BabyPropBags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BabyPropRecords_BabyProps_BabyPropId",
                table: "BabyPropRecords",
                column: "BabyPropId",
                principalTable: "BabyProps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyPropRecords_BabyPropBags_BabyPropBagId1",
                table: "BabyPropRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_BabyPropRecords_BabyProps_BabyPropId",
                table: "BabyPropRecords");

            migrationBuilder.DropIndex(
                name: "IX_BabyPropRecords_BabyPropBagId1",
                table: "BabyPropRecords");

            migrationBuilder.DropColumn(
                name: "BabyPropBagId",
                table: "BabyPropRecords");

            migrationBuilder.DropColumn(
                name: "BabyPropBagId1",
                table: "BabyPropRecords");

            migrationBuilder.DropColumn(
                name: "Img",
                table: "BabyPropBags");

            migrationBuilder.DropColumn(
                name: "PriceGoldCoin",
                table: "BabyPropBags");

            migrationBuilder.DropColumn(
                name: "PriceRMB",
                table: "BabyPropBags");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "BabyPropBags");

            migrationBuilder.DropColumn(
                name: "isDisabled",
                table: "BabyPropBags");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "BabyPropId",
                table: "BabyPropRecords",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BabyPropRecords_BabyProps_BabyPropId",
                table: "BabyPropRecords",
                column: "BabyPropId",
                principalTable: "BabyProps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

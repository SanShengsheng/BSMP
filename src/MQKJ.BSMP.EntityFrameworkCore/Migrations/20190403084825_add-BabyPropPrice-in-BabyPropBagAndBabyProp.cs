using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addBabyPropPriceinBabyPropBagAndBabyProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyPropPrices_BabyPropBags_BabyPropBagId",
                table: "BabyPropPrices");

            migrationBuilder.DropIndex(
                name: "IX_BabyPropPrices_BabyPropBagId",
                table: "BabyPropPrices");

            migrationBuilder.DropColumn(
                name: "BabyPropBagId",
                table: "BabyPropPrices");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "BabyPropPriceId",
                table: "BabyPropBagAndBabyProps",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BabyPropBagAndBabyProps_BabyPropPriceId",
                table: "BabyPropBagAndBabyProps",
                column: "BabyPropPriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_BabyPropBagAndBabyProps_BabyPropPrices_BabyPropPriceId",
                table: "BabyPropBagAndBabyProps",
                column: "BabyPropPriceId",
                principalTable: "BabyPropPrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyPropBagAndBabyProps_BabyPropPrices_BabyPropPriceId",
                table: "BabyPropBagAndBabyProps");

            migrationBuilder.DropIndex(
                name: "IX_BabyPropBagAndBabyProps_BabyPropPriceId",
                table: "BabyPropBagAndBabyProps");

            migrationBuilder.DropColumn(
                name: "BabyPropPriceId",
                table: "BabyPropBagAndBabyProps");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "BabyPropBagId",
                table: "BabyPropPrices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BabyPropPrices_BabyPropBagId",
                table: "BabyPropPrices",
                column: "BabyPropBagId");

            migrationBuilder.AddForeignKey(
                name: "FK_BabyPropPrices_BabyPropBags_BabyPropBagId",
                table: "BabyPropPrices",
                column: "BabyPropBagId",
                principalTable: "BabyPropBags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

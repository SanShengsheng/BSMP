using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addbabyPropBagAndBabyPropstable01 : Migration
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
                oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "BabyPropBagId",
                table: "BabyPropRecords",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BabyPropRecords_BabyPropBagId",
                table: "BabyPropRecords",
                column: "BabyPropBagId");

            migrationBuilder.AddForeignKey(
                name: "FK_BabyPropRecords_BabyPropBags_BabyPropBagId",
                table: "BabyPropRecords",
                column: "BabyPropBagId",
                principalTable: "BabyPropBags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyPropRecords_BabyPropBags_BabyPropBagId",
                table: "BabyPropRecords");

            migrationBuilder.DropIndex(
                name: "IX_BabyPropRecords_BabyPropBagId",
                table: "BabyPropRecords");

            migrationBuilder.DropColumn(
                name: "BabyPropBagId",
                table: "BabyPropRecords");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));
        }
    }
}

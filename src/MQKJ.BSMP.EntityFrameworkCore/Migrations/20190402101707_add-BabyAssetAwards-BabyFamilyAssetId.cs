using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addBabyAssetAwardsBabyFamilyAssetId : Migration
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
                name: "BabyFamilyAssetId",
                table: "BabyAssetAwards",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BabyAssetAwards_BabyFamilyAssetId",
                table: "BabyAssetAwards",
                column: "BabyFamilyAssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_BabyAssetAwards_BabyFamilyAssets_BabyFamilyAssetId",
                table: "BabyAssetAwards",
                column: "BabyFamilyAssetId",
                principalTable: "BabyFamilyAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyAssetAwards_BabyFamilyAssets_BabyFamilyAssetId",
                table: "BabyAssetAwards");

            migrationBuilder.DropIndex(
                name: "IX_BabyAssetAwards_BabyFamilyAssetId",
                table: "BabyAssetAwards");

            migrationBuilder.DropColumn(
                name: "BabyFamilyAssetId",
                table: "BabyAssetAwards");

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

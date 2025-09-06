using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class removeBabyAssetAwardsBabyFamilyAssetId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyAssetAwards_BabyFamilyAssets_BabyFamilyAssetId1",
                table: "BabyAssetAwards");

            migrationBuilder.DropForeignKey(
                name: "FK_BabyAssetFeatureRecords_BabyProps_BabyPropId",
                table: "BabyAssetFeatureRecords");

            migrationBuilder.DropIndex(
                name: "IX_BabyAssetAwards_BabyFamilyAssetId1",
                table: "BabyAssetAwards");

            //migrationBuilder.DropColumn(
            //    name: "FamilyId",
            //    table: "FightRecords");

            //migrationBuilder.DropColumn(
            //    name: "WinnerId",
            //    table: "FightRecords");

            //migrationBuilder.DropColumn(
            //    name: "failederId",
            //    table: "FightRecords");

            migrationBuilder.DropColumn(
                name: "BabyFamilyAssetId",
                table: "BabyAssetAwards");

            migrationBuilder.DropColumn(
                name: "BabyFamilyAssetId1",
                table: "BabyAssetAwards");

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "EndTime",
            //    table: "RunHorseInformations",
            //    nullable: true,
            //    defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
            //    oldClrType: typeof(DateTime),
            //    oldNullable: true,
            //    oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));

            //migrationBuilder.AddColumn<double>(
            //    name: "AttributeRate",
            //    table: "FightRecords",
            //    nullable: false,
            //    defaultValue: 0.0);

            //migrationBuilder.AddColumn<int>(
            //    name: "DanGrading",
            //    table: "Competitions",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "BabyPropId",
                table: "BabyAssetFeatureRecords",
                nullable: true,
                oldClrType: typeof(int));

            //migrationBuilder.AddColumn<int>(
            //    name: "Code",
            //    table: "AthleticsRewards",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_BabyAssetFeatureRecords_BabyProps_BabyPropId",
                table: "BabyAssetFeatureRecords",
                column: "BabyPropId",
                principalTable: "BabyProps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyAssetFeatureRecords_BabyProps_BabyPropId",
                table: "BabyAssetFeatureRecords");

            //migrationBuilder.DropColumn(
            //    name: "AttributeRate",
            //    table: "FightRecords");

            //migrationBuilder.DropColumn(
            //    name: "DanGrading",
            //    table: "Competitions");

            //migrationBuilder.DropColumn(
            //    name: "Code",
            //    table: "AthleticsRewards");

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "EndTime",
            //    table: "RunHorseInformations",
            //    nullable: true,
            //    defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
            //    oldClrType: typeof(DateTime),
            //    oldNullable: true,
            //    oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));

            //migrationBuilder.AddColumn<int>(
            //    name: "FamilyId",
            //    table: "FightRecords",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "WinnerId",
            //    table: "FightRecords",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "failederId",
            //    table: "FightRecords",
            //    nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BabyPropId",
                table: "BabyAssetFeatureRecords",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BabyFamilyAssetId",
                table: "BabyAssetAwards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "BabyFamilyAssetId1",
                table: "BabyAssetAwards",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BabyAssetAwards_BabyFamilyAssetId1",
                table: "BabyAssetAwards",
                column: "BabyFamilyAssetId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BabyAssetAwards_BabyFamilyAssets_BabyFamilyAssetId1",
                table: "BabyAssetAwards",
                column: "BabyFamilyAssetId1",
                principalTable: "BabyFamilyAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BabyAssetFeatureRecords_BabyProps_BabyPropId",
                table: "BabyAssetFeatureRecords",
                column: "BabyPropId",
                principalTable: "BabyProps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

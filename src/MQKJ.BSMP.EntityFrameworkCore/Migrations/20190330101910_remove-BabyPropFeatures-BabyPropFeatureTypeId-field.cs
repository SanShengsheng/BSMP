using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class removeBabyPropFeaturesBabyPropFeatureTypeIdfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyPropFeatures_BabyPropFeatureTypes_BabyPropFeatureTypeId1",
                table: "BabyPropFeatures");

            migrationBuilder.DropIndex(
                name: "IX_BabyPropFeatures_BabyPropFeatureTypeId1",
                table: "BabyPropFeatures");

            migrationBuilder.DropColumn(
                name: "BabyPropFeatureTypeId",
                table: "BabyPropFeatures");

            migrationBuilder.DropColumn(
                name: "BabyPropFeatureTypeId1",
                table: "BabyPropFeatures");

            //migrationBuilder.DropColumn(
            //    name: "BabyPropId",
            //    table: "AthleticsRewardRecords");

            //migrationBuilder.DropColumn(
            //    name: "BabyPropPriceId",
            //    table: "AthleticsRewardRecords");

            //migrationBuilder.RenameColumn(
            //    name: "CoinCount",
            //    table: "AthleticsRewardRecords",
            //    newName: "AthleticsRewardId");

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "EndTime",
            //    table: "RunHorseInformations",
            //    nullable: true,
            //    defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
            //    oldClrType: typeof(DateTime),
            //    oldNullable: true,
            //    oldDefaultValue: new DateTime(2119, 3, 29, 12, 11, 13, 434, DateTimeKind.Local));

            //migrationBuilder.AddColumn<double>(
            //    name: "PropAdditionRate",
            //    table: "FightRecords",
            //    nullable: false,
            //    defaultValue: 0.0);

            //migrationBuilder.AddColumn<int>(
            //    name: "BabyPropCount",
            //    table: "AthleticsRewards",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_AthleticsRewardRecords_AthleticsRewardId",
            //    table: "AthleticsRewardRecords",
            //    column: "AthleticsRewardId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AthleticsRewardRecords_AthleticsRewards_AthleticsRewardId",
            //    table: "AthleticsRewardRecords",
            //    column: "AthleticsRewardId",
            //    principalTable: "AthleticsRewards",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_AthleticsRewardRecords_AthleticsRewards_AthleticsRewardId",
            //    table: "AthleticsRewardRecords");

            //migrationBuilder.DropIndex(
            //    name: "IX_AthleticsRewardRecords_AthleticsRewardId",
            //    table: "AthleticsRewardRecords");

            //migrationBuilder.DropColumn(
            //    name: "PropAdditionRate",
            //    table: "FightRecords");

            //migrationBuilder.DropColumn(
            //    name: "BabyPropCount",
            //    table: "AthleticsRewards");

            //migrationBuilder.RenameColumn(
            //    name: "AthleticsRewardId",
            //    table: "AthleticsRewardRecords",
            //    newName: "CoinCount");

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "EndTime",
            //    table: "RunHorseInformations",
            //    nullable: true,
            //    defaultValue: new DateTime(2119, 3, 29, 12, 11, 13, 434, DateTimeKind.Local),
            //    oldClrType: typeof(DateTime),
            //    oldNullable: true,
            //    oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "BabyPropFeatureTypeId",
                table: "BabyPropFeatures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "BabyPropFeatureTypeId1",
                table: "BabyPropFeatures",
                nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "BabyPropId",
            //    table: "AthleticsRewardRecords",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "BabyPropPriceId",
            //    table: "AthleticsRewardRecords",
            //    nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BabyPropFeatures_BabyPropFeatureTypeId1",
                table: "BabyPropFeatures",
                column: "BabyPropFeatureTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BabyPropFeatures_BabyPropFeatureTypes_BabyPropFeatureTypeId1",
                table: "BabyPropFeatures",
                column: "BabyPropFeatureTypeId1",
                principalTable: "BabyPropFeatureTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

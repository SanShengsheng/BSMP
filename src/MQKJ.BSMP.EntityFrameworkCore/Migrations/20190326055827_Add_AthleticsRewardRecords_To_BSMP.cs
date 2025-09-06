using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Add_AthleticsRewardRecords_To_BSMP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BabyId",
                table: "AthleticsRewards");

            migrationBuilder.DropColumn(
                name: "ChineseBabyMarketId",
                table: "AthleticsRewards");

            migrationBuilder.DropColumn(
                name: "FamilyId",
                table: "AthleticsRewards");

            migrationBuilder.RenameColumn(
                name: "RewardType",
                table: "AthleticsRewards",
                newName: "CoinCount");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(2119, 3, 26, 13, 58, 25, 386, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2119, 3, 25, 17, 4, 55, 599, DateTimeKind.Local));

            migrationBuilder.AddColumn<int>(
                name: "BabyPropId",
                table: "AthleticsRewards",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BabyPropPriceId",
                table: "AthleticsRewards",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AthleticsRewardRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BabyId = table.Column<int>(nullable: true),
                    FamilyId = table.Column<int>(nullable: true),
                    BabyPropId = table.Column<int>(nullable: true),
                    CoinCount = table.Column<int>(nullable: false),
                    BabyPropPriceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AthleticsRewardRecords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AthleticsRewards_BabyPropId",
                table: "AthleticsRewards",
                column: "BabyPropId");

            migrationBuilder.CreateIndex(
                name: "IX_AthleticsRewards_BabyPropPriceId",
                table: "AthleticsRewards",
                column: "BabyPropPriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AthleticsRewards_BabyProps_BabyPropId",
                table: "AthleticsRewards",
                column: "BabyPropId",
                principalTable: "BabyProps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AthleticsRewards_BabyPropPrices_BabyPropPriceId",
                table: "AthleticsRewards",
                column: "BabyPropPriceId",
                principalTable: "BabyPropPrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AthleticsRewards_BabyProps_BabyPropId",
                table: "AthleticsRewards");

            migrationBuilder.DropForeignKey(
                name: "FK_AthleticsRewards_BabyPropPrices_BabyPropPriceId",
                table: "AthleticsRewards");

            migrationBuilder.DropTable(
                name: "AthleticsRewardRecords");

            migrationBuilder.DropIndex(
                name: "IX_AthleticsRewards_BabyPropId",
                table: "AthleticsRewards");

            migrationBuilder.DropIndex(
                name: "IX_AthleticsRewards_BabyPropPriceId",
                table: "AthleticsRewards");

            migrationBuilder.DropColumn(
                name: "BabyPropId",
                table: "AthleticsRewards");

            migrationBuilder.DropColumn(
                name: "BabyPropPriceId",
                table: "AthleticsRewards");

            migrationBuilder.RenameColumn(
                name: "CoinCount",
                table: "AthleticsRewards",
                newName: "RewardType");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(2119, 3, 25, 17, 4, 55, 599, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2119, 3, 26, 13, 58, 25, 386, DateTimeKind.Local));

            migrationBuilder.AddColumn<int>(
                name: "BabyId",
                table: "AthleticsRewards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChineseBabyMarketId",
                table: "AthleticsRewards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FamilyId",
                table: "AthleticsRewards",
                nullable: false,
                defaultValue: 0);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addbabyPropBagtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Professions");

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

            migrationBuilder.AddColumn<int>(
                name: "BabyPropPriceId",
                table: "BabyFamilyAssets",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BabyPropBags",
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
                    Code = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    BabyPropId = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    CurrencyCount = table.Column<double>(nullable: false),
                    CurrencyType = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    NextPropBagId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyPropBags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyPropBags_BabyProps_BabyPropId",
                        column: x => x.BabyPropId,
                        principalTable: "BabyProps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BabyPropPrices_BabyPropBagId",
                table: "BabyPropPrices",
                column: "BabyPropBagId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyFamilyAssets_BabyPropPriceId",
                table: "BabyFamilyAssets",
                column: "BabyPropPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyPropBags_BabyPropId",
                table: "BabyPropBags",
                column: "BabyPropId");

            migrationBuilder.AddForeignKey(
                name: "FK_BabyFamilyAssets_BabyPropPrices_BabyPropPriceId",
                table: "BabyFamilyAssets",
                column: "BabyPropPriceId",
                principalTable: "BabyPropPrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BabyPropPrices_BabyPropBags_BabyPropBagId",
                table: "BabyPropPrices",
                column: "BabyPropBagId",
                principalTable: "BabyPropBags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyFamilyAssets_BabyPropPrices_BabyPropPriceId",
                table: "BabyFamilyAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_BabyPropPrices_BabyPropBags_BabyPropBagId",
                table: "BabyPropPrices");

            migrationBuilder.DropTable(
                name: "BabyPropBags");

            migrationBuilder.DropIndex(
                name: "IX_BabyPropPrices_BabyPropBagId",
                table: "BabyPropPrices");

            migrationBuilder.DropIndex(
                name: "IX_BabyFamilyAssets_BabyPropPriceId",
                table: "BabyFamilyAssets");

            migrationBuilder.DropColumn(
                name: "BabyPropBagId",
                table: "BabyPropPrices");

            migrationBuilder.DropColumn(
                name: "BabyPropPriceId",
                table: "BabyFamilyAssets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Professions",
                nullable: false,
                defaultValue: 0);
        }
    }
}

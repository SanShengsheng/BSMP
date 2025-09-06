using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addbabyPropBagAndBabyPropstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyPropBags_BabyProps_BabyPropId",
                table: "BabyPropBags");

            migrationBuilder.DropForeignKey(
                name: "FK_BabyPropRecords_BabyPropBags_BabyPropBagId1",
                table: "BabyPropRecords");

            migrationBuilder.DropIndex(
                name: "IX_BabyPropRecords_BabyPropBagId1",
                table: "BabyPropRecords");

            migrationBuilder.DropIndex(
                name: "IX_BabyPropBags_BabyPropId",
                table: "BabyPropBags");

            migrationBuilder.DropColumn(
                name: "BabyPropBagId",
                table: "BabyPropRecords");

            migrationBuilder.DropColumn(
                name: "BabyPropBagId1",
                table: "BabyPropRecords");

            migrationBuilder.DropColumn(
                name: "BabyPropId",
                table: "BabyPropBags");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Aside",
                table: "BabyEndings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "BabyEndings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BabyPropBagAndBabyProps",
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
                    BabyPropId = table.Column<int>(nullable: false),
                    BabyPropBagId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyPropBagAndBabyProps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyPropBagAndBabyProps_BabyPropBags_BabyPropBagId",
                        column: x => x.BabyPropBagId,
                        principalTable: "BabyPropBags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BabyPropBagAndBabyProps_BabyProps_BabyPropId",
                        column: x => x.BabyPropId,
                        principalTable: "BabyProps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BabyPropBagAndBabyProps_BabyPropBagId",
                table: "BabyPropBagAndBabyProps",
                column: "BabyPropBagId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyPropBagAndBabyProps_BabyPropId",
                table: "BabyPropBagAndBabyProps",
                column: "BabyPropId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BabyPropBagAndBabyProps");

            migrationBuilder.DropColumn(
                name: "Aside",
                table: "BabyEndings");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "BabyEndings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "BabyPropBagId",
                table: "BabyPropRecords",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BabyPropBagId1",
                table: "BabyPropRecords",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BabyPropId",
                table: "BabyPropBags",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BabyPropRecords_BabyPropBagId1",
                table: "BabyPropRecords",
                column: "BabyPropBagId1");

            migrationBuilder.CreateIndex(
                name: "IX_BabyPropBags_BabyPropId",
                table: "BabyPropBags",
                column: "BabyPropId");

            migrationBuilder.AddForeignKey(
                name: "FK_BabyPropBags_BabyProps_BabyPropId",
                table: "BabyPropBags",
                column: "BabyPropId",
                principalTable: "BabyProps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BabyPropRecords_BabyPropBags_BabyPropBagId1",
                table: "BabyPropRecords",
                column: "BabyPropBagId1",
                principalTable: "BabyPropBags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

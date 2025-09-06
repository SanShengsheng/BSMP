using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class updateStakeholderIdguidtoguidnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyPropRecords_Players_PurchaserId",
                table: "BabyPropRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_FamilyCoinDepositChangeRecords_Players_StakeholderId",
                table: "FamilyCoinDepositChangeRecords");

            migrationBuilder.AlterColumn<Guid>(
                name: "StakeholderId",
                table: "FamilyCoinDepositChangeRecords",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "PurchaserId",
                table: "BabyPropRecords",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_BabyPropRecords_Players_PurchaserId",
                table: "BabyPropRecords",
                column: "PurchaserId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyCoinDepositChangeRecords_Players_StakeholderId",
                table: "FamilyCoinDepositChangeRecords",
                column: "StakeholderId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyPropRecords_Players_PurchaserId",
                table: "BabyPropRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_FamilyCoinDepositChangeRecords_Players_StakeholderId",
                table: "FamilyCoinDepositChangeRecords");

            migrationBuilder.AlterColumn<Guid>(
                name: "StakeholderId",
                table: "FamilyCoinDepositChangeRecords",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PurchaserId",
                table: "BabyPropRecords",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BabyPropRecords_Players_PurchaserId",
                table: "BabyPropRecords",
                column: "PurchaserId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyCoinDepositChangeRecords_Players_StakeholderId",
                table: "FamilyCoinDepositChangeRecords",
                column: "StakeholderId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_DismissFamilyRecord_For_Add_Family : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireTime",
                table: "DismissFamilyRecords",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_DismissFamilyRecords_FamilyId",
                table: "DismissFamilyRecords",
                column: "FamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_DismissFamilyRecords_Families_FamilyId",
                table: "DismissFamilyRecords",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DismissFamilyRecords_Families_FamilyId",
                table: "DismissFamilyRecords");

            migrationBuilder.DropIndex(
                name: "IX_DismissFamilyRecords_FamilyId",
                table: "DismissFamilyRecords");

            migrationBuilder.DropColumn(
                name: "ExpireTime",
                table: "DismissFamilyRecords");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class updateBabyPropBabyPropPropertyAwardId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BabyPropPropertyAwardId",
                table: "BabyProps",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BabyProps_BabyPropPropertyAwardId",
                table: "BabyProps",
                column: "BabyPropPropertyAwardId");

            migrationBuilder.AddForeignKey(
                name: "FK_BabyProps_BabyPropPropertyAwards_BabyPropPropertyAwardId",
                table: "BabyProps",
                column: "BabyPropPropertyAwardId",
                principalTable: "BabyPropPropertyAwards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyProps_BabyPropPropertyAwards_BabyPropPropertyAwardId",
                table: "BabyProps");

            migrationBuilder.DropIndex(
                name: "IX_BabyProps_BabyPropPropertyAwardId",
                table: "BabyProps");

            migrationBuilder.DropColumn(
                name: "BabyPropPropertyAwardId",
                table: "BabyProps");
        }
    }
}

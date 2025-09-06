using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class removebabypropsbabyPropPropertyAwardId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyProps_BabyPropPropertyAwards_BabyPropPropertyAwardId1",
                table: "BabyProps");

            migrationBuilder.DropIndex(
                name: "IX_BabyProps_BabyPropPropertyAwardId1",
                table: "BabyProps");

            migrationBuilder.DropColumn(
                name: "BabyPropPropertyAwardId",
                table: "BabyProps");

            migrationBuilder.DropColumn(
                name: "BabyPropPropertyAwardId1",
                table: "BabyProps");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BabyPropPropertyAwardId",
                table: "BabyProps",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BabyPropPropertyAwardId1",
                table: "BabyProps",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BabyProps_BabyPropPropertyAwardId1",
                table: "BabyProps",
                column: "BabyPropPropertyAwardId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BabyProps_BabyPropPropertyAwards_BabyPropPropertyAwardId1",
                table: "BabyProps",
                column: "BabyPropPropertyAwardId1",
                principalTable: "BabyPropPropertyAwards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

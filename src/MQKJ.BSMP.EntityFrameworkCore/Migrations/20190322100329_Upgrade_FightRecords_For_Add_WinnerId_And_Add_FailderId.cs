using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_FightRecords_For_Add_WinnerId_And_Add_FailderId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WinnerId",
                table: "FightRecords",
                nullable: true,
                defaultValue: null);


            migrationBuilder.AddColumn<int>(
                name: "failederId",
                table: "FightRecords",
                nullable: true,
                defaultValue: null);

            migrationBuilder.AddColumn<int>(
               name: "RewardPlayerCount",
               table: "SeasonManagements",
               nullable: false,
               defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
              name: "WinnerId",
              table: "FightRecords");


            migrationBuilder.DropColumn(
               name: "failederId",
               table: "FightRecords");

            migrationBuilder.DropColumn(
               name: "RewardPlayerCount",
               table: "SeasonManagements");
        }
    }
}

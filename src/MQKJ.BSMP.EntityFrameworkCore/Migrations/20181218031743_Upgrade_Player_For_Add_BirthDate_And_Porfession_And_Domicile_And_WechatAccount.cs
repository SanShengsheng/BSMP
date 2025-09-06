using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_Player_For_Add_BirthDate_And_Porfession_And_Domicile_And_WechatAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Players",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Domicile",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeChatAccount",
                table: "Players",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Domicile",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Profession",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "WeChatAccount",
                table: "Players");
        }
    }
}

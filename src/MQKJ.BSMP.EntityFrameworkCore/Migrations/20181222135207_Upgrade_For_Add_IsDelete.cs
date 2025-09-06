using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_For_Add_IsDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "PlayerLabels",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "PlayerLabels",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PlayerLabels",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "LoveCards",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "LoveCards",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LoveCards",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "LoveCardOptions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "LoveCardOptions",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LoveCardOptions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "PlayerLabels");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "PlayerLabels");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PlayerLabels");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "LoveCards");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "LoveCards");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LoveCards");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "LoveCardOptions");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "LoveCardOptions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LoveCardOptions");
        }
    }
}

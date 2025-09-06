using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_MqAgent_Modify_PlayerId_EnableNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MqAgents_Players_PlayerId",
                table: "MqAgents");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlayerId",
                table: "MqAgents",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_MqAgents_Players_PlayerId",
                table: "MqAgents",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MqAgents_Players_PlayerId",
                table: "MqAgents");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlayerId",
                table: "MqAgents",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MqAgents_Players_PlayerId",
                table: "MqAgents",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

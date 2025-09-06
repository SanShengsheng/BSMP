using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addgameTaskinviterandinviteefroeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_AbpUsers_AuditorId",
                table: "Questions");

            migrationBuilder.AlterColumn<long>(
                name: "AuditorId",
                table: "Questions",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AuditDateTime",
                table: "Questions",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<Guid>(
                name: "InviteePlayerId",
                table: "GameTasks",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_GameTasks_InviteePlayerId",
                table: "GameTasks",
                column: "InviteePlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_GameTasks_InviterPlayerId",
                table: "GameTasks",
                column: "InviterPlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameTasks_Players_InviteePlayerId",
                table: "GameTasks",
                column: "InviteePlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameTasks_Players_InviterPlayerId",
                table: "GameTasks",
                column: "InviterPlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_AbpUsers_AuditorId",
                table: "Questions",
                column: "AuditorId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameTasks_Players_InviteePlayerId",
                table: "GameTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_GameTasks_Players_InviterPlayerId",
                table: "GameTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_AbpUsers_AuditorId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_GameTasks_InviteePlayerId",
                table: "GameTasks");

            migrationBuilder.DropIndex(
                name: "IX_GameTasks_InviterPlayerId",
                table: "GameTasks");

            migrationBuilder.AlterColumn<long>(
                name: "AuditorId",
                table: "Questions",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AuditDateTime",
                table: "Questions",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "InviteePlayerId",
                table: "GameTasks",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_AbpUsers_AuditorId",
                table: "Questions",
                column: "AuditorId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

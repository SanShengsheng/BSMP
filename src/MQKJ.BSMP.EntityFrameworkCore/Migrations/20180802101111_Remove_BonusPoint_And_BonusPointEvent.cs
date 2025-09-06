using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Remove_BonusPoint_And_BonusPointEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_AbpUsers_AuditorId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "BonusPoints");

            migrationBuilder.DropTable(
                name: "BonusPoinEvents");

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
                name: "FK_Questions_AbpUsers_AuditorId",
                table: "Questions");

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

            migrationBuilder.CreateTable(
                name: "BonusPoinEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    EventName = table.Column<string>(maxLength: 120, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Sort = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusPoinEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BonusPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    EventId = table.Column<Guid>(nullable: false),
                    ExtensionFiled = table.Column<string>(nullable: true),
                    GatherCount = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    PlayerId = table.Column<Guid>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonusPoints_BonusPoinEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "BonusPoinEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BonusPoints_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BonusPoints_EventId",
                table: "BonusPoints",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_BonusPoints_PlayerId",
                table: "BonusPoints",
                column: "PlayerId");

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

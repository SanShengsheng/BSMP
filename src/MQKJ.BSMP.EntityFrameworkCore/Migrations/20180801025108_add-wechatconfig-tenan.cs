using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addwechatconfigtenan : Migration
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

            migrationBuilder.AddColumn<bool>(
                name: "IsApplyWechat",
                table: "AbpTenants",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "WechatAppId",
                table: "AbpTenants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WechatAppSecret",
                table: "AbpTenants",
                nullable: true);

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

            migrationBuilder.DropColumn(
                name: "IsApplyWechat",
                table: "AbpTenants");

            migrationBuilder.DropColumn(
                name: "WechatAppId",
                table: "AbpTenants");

            migrationBuilder.DropColumn(
                name: "WechatAppSecret",
                table: "AbpTenants");

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

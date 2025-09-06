using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addquestioncreatorandauditorandauditDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_SceneFiles_FileId",
            //    table: "SceneFiles");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_QuestionTags",
            //    table: "QuestionTags");

            //migrationBuilder.AddColumn<int>(
            //    name: "Id",
            //    table: "QuestionTags",
            //    nullable: false,
            //    defaultValue: 0)
            //    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "CreationTime",
            //    table: "QuestionTags",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            //migrationBuilder.AddColumn<long>(
            //    name: "CreatorUserId",
            //    table: "QuestionTags",
            //    nullable: true);

            //migrationBuilder.AddColumn<long>(
            //    name: "DeleterUserId",
            //    table: "QuestionTags",
            //    nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "DeletionTime",
            //    table: "QuestionTags",
            //    nullable: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsDeleted",
            //    table: "QuestionTags",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "LastModificationTime",
            //    table: "QuestionTags",
            //    nullable: true);

            //migrationBuilder.AddColumn<long>(
            //    name: "LastModifierUserId",
            //    table: "QuestionTags",
            //    nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AuditDateTime",
                table: "Questions",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "AuditorId",
                table: "Questions",
                nullable: true,
                defaultValue: 0L);

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_QuestionTags",
            //    table: "QuestionTags",
            //    column: "Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SceneFiles_FileId",
            //    table: "SceneFiles",
            //    column: "FileId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_QuestionTags_QuestionId",
            //    table: "QuestionTags",
            //    column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_AuditorId",
                table: "Questions",
                column: "AuditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CreatorUserId",
                table: "Questions",
                column: "CreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_AbpUsers_AuditorId",
                table: "Questions",
                column: "AuditorId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_AbpUsers_CreatorUserId",
                table: "Questions",
                column: "CreatorUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_AbpUsers_AuditorId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_AbpUsers_CreatorUserId",
                table: "Questions");

            //migrationBuilder.DropIndex(
            //    name: "IX_SceneFiles_FileId",
            //    table: "SceneFiles");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_QuestionTags",
            //    table: "QuestionTags");

            //migrationBuilder.DropIndex(
            //    name: "IX_QuestionTags_QuestionId",
            //    table: "QuestionTags");

            migrationBuilder.DropIndex(
                name: "IX_Questions_AuditorId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_CreatorUserId",
                table: "Questions");

            //migrationBuilder.DropColumn(
            //    name: "Id",
            //    table: "QuestionTags");

            //migrationBuilder.DropColumn(
            //    name: "CreationTime",
            //    table: "QuestionTags");

            //migrationBuilder.DropColumn(
            //    name: "CreatorUserId",
            //    table: "QuestionTags");

            //migrationBuilder.DropColumn(
            //    name: "DeleterUserId",
            //    table: "QuestionTags");

            //migrationBuilder.DropColumn(
            //    name: "DeletionTime",
            //    table: "QuestionTags");

            //migrationBuilder.DropColumn(
            //    name: "IsDeleted",
            //    table: "QuestionTags");

            //migrationBuilder.DropColumn(
            //    name: "LastModificationTime",
            //    table: "QuestionTags");

            //migrationBuilder.DropColumn(
            //    name: "LastModifierUserId",
            //table: "QuestionTags");

            migrationBuilder.DropColumn(
                name: "AuditDateTime",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "AuditorId",
                table: "Questions");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_QuestionTags",
            //    table: "QuestionTags",
            //    columns: new[] { "QuestionId", "TagId" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_SceneFiles_FileId",
            //    table: "SceneFiles",
            //    column: "FileId");
        }
    }
}

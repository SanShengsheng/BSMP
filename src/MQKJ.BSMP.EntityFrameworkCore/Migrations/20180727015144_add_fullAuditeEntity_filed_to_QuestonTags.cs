using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class add_fullAuditeEntity_filed_to_QuestonTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionTags",
                table: "QuestionTags");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "QuestionTags",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "QuestionTags",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "QuestionTags",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "QuestionTags",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "QuestionTags",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "QuestionTags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "QuestionTags",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "QuestionTags",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionTags",
                table: "QuestionTags",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTags_QuestionId",
                table: "QuestionTags",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionTags",
                table: "QuestionTags");

            migrationBuilder.DropIndex(
                name: "IX_QuestionTags_QuestionId",
                table: "QuestionTags");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "QuestionTags");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "QuestionTags");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "QuestionTags");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "QuestionTags");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "QuestionTags");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "QuestionTags");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "QuestionTags");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "QuestionTags");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionTags",
                table: "QuestionTags",
                columns: new[] { "QuestionId", "TagId" });
        }
    }
}

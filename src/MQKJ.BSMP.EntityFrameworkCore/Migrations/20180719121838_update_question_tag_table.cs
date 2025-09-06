using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class update_question_tag_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionTags",
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

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TagTypeId",
                table: "Tags",
                column: "TagTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTags_TagId",
                table: "QuestionTags",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTags_Questions_QuestionId",
                table: "QuestionTags",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTags_Tags_TagId",
                table: "QuestionTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_TagTypes_TagTypeId",
                table: "Tags",
                column: "TagTypeId",
                principalTable: "TagTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTags_Questions_QuestionId",
                table: "QuestionTags");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTags_Tags_TagId",
                table: "QuestionTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_TagTypes_TagTypeId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_TagTypeId",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionTags",
                table: "QuestionTags");

            migrationBuilder.DropIndex(
                name: "IX_QuestionTags_TagId",
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
        }
    }
}

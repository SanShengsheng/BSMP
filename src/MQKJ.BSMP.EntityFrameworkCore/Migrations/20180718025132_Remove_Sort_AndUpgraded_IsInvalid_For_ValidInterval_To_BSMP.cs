using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Remove_Sort_AndUpgraded_IsInvalid_For_ValidInterval_To_BSMP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInvalid",
                table: "GameTasks");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "AnswerQuestions");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "AnswerQuestions");

            migrationBuilder.RenameColumn(
                name: "Sort",
                table: "GameTasks",
                newName: "ValidInterval");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValidInterval",
                table: "GameTasks",
                newName: "Sort");

            migrationBuilder.AddColumn<bool>(
                name: "IsInvalid",
                table: "GameTasks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "AnswerQuestions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "AnswerQuestions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

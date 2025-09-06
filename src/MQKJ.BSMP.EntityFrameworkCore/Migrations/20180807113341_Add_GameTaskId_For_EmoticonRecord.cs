using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Add_GameTaskId_For_EmoticonRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GameTaskId",
                table: "EmoticonRecords",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_EmoticonRecords_GameTaskId",
                table: "EmoticonRecords",
                column: "GameTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmoticonRecords_GameTasks_GameTaskId",
                table: "EmoticonRecords",
                column: "GameTaskId",
                principalTable: "GameTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmoticonRecords_GameTasks_GameTaskId",
                table: "EmoticonRecords");

            migrationBuilder.DropIndex(
                name: "IX_EmoticonRecords_GameTaskId",
                table: "EmoticonRecords");

            migrationBuilder.DropColumn(
                name: "GameTaskId",
                table: "EmoticonRecords");
        }
    }
}

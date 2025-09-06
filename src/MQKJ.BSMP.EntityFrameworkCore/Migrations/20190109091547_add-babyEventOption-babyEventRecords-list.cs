using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addbabyEventOptionbabyEventRecordslist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BabyEventRecords_EventId",
                table: "BabyEventRecords",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyEventRecords_OptionId",
                table: "BabyEventRecords",
                column: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_BabyEventRecords_BabyEvents_EventId",
                table: "BabyEventRecords",
                column: "EventId",
                principalTable: "BabyEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BabyEventRecords_BabyEventOptions_OptionId",
                table: "BabyEventRecords",
                column: "OptionId",
                principalTable: "BabyEventOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyEventRecords_BabyEvents_EventId",
                table: "BabyEventRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_BabyEventRecords_BabyEventOptions_OptionId",
                table: "BabyEventRecords");

            migrationBuilder.DropIndex(
                name: "IX_BabyEventRecords_EventId",
                table: "BabyEventRecords");

            migrationBuilder.DropIndex(
                name: "IX_BabyEventRecords_OptionId",
                table: "BabyEventRecords");
        }
    }
}

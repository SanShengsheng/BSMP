using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addtablemqagent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventGroupBabyEvents_BabyEvents_BabyEventId",
                table: "EventGroupBabyEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_EventGroupBabyEvents_EventGroups_EventGroupId",
                table: "EventGroupBabyEvents");

            migrationBuilder.DropIndex(
                name: "IX_EventGroupBabyEvents_BabyEventId",
                table: "EventGroupBabyEvents");

            migrationBuilder.DropIndex(
                name: "IX_EventGroupBabyEvents_EventGroupId",
                table: "EventGroupBabyEvents");

            migrationBuilder.DropColumn(
                name: "BabyEventId",
                table: "EventGroupBabyEvents");

            migrationBuilder.DropColumn(
                name: "EventGroupId",
                table: "EventGroupBabyEvents");

            migrationBuilder.AddColumn<int>(
                name: "CoinCount",
                table: "Rewards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EventGroupBabyEvents_EventId",
                table: "EventGroupBabyEvents",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventGroupBabyEvents_GroupId",
                table: "EventGroupBabyEvents",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventGroupBabyEvents_BabyEvents_EventId",
                table: "EventGroupBabyEvents",
                column: "EventId",
                principalTable: "BabyEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventGroupBabyEvents_EventGroups_GroupId",
                table: "EventGroupBabyEvents",
                column: "GroupId",
                principalTable: "EventGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventGroupBabyEvents_BabyEvents_EventId",
                table: "EventGroupBabyEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_EventGroupBabyEvents_EventGroups_GroupId",
                table: "EventGroupBabyEvents");

            migrationBuilder.DropIndex(
                name: "IX_EventGroupBabyEvents_EventId",
                table: "EventGroupBabyEvents");

            migrationBuilder.DropIndex(
                name: "IX_EventGroupBabyEvents_GroupId",
                table: "EventGroupBabyEvents");

            migrationBuilder.DropColumn(
                name: "CoinCount",
                table: "Rewards");

            migrationBuilder.AddColumn<int>(
                name: "BabyEventId",
                table: "EventGroupBabyEvents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventGroupId",
                table: "EventGroupBabyEvents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventGroupBabyEvents_BabyEventId",
                table: "EventGroupBabyEvents",
                column: "BabyEventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventGroupBabyEvents_EventGroupId",
                table: "EventGroupBabyEvents",
                column: "EventGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventGroupBabyEvents_BabyEvents_BabyEventId",
                table: "EventGroupBabyEvents",
                column: "BabyEventId",
                principalTable: "BabyEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventGroupBabyEvents_EventGroups_EventGroupId",
                table: "EventGroupBabyEvents",
                column: "EventGroupId",
                principalTable: "EventGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addbabyEventIsEmergenciesfiled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyEvents_EventGroups_EventGroupId",
                table: "BabyEvents");

            migrationBuilder.DropIndex(
                name: "IX_BabyEvents_EventGroupId",
                table: "BabyEvents");

            migrationBuilder.DropColumn(
                name: "EventGroupId",
                table: "BabyEvents");

            migrationBuilder.AddColumn<bool>(
                name: "IsEmergencies",
                table: "BabyEvents",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmergencies",
                table: "BabyEvents");

            migrationBuilder.AddColumn<int>(
                name: "EventGroupId",
                table: "BabyEvents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BabyEvents_EventGroupId",
                table: "BabyEvents",
                column: "EventGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_BabyEvents_EventGroups_EventGroupId",
                table: "BabyEvents",
                column: "EventGroupId",
                principalTable: "EventGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_Baby_For_Add_GroupId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Babies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Babies_GroupId",
                table: "Babies",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Babies_EventGroups_GroupId",
                table: "Babies",
                column: "GroupId",
                principalTable: "EventGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Babies_EventGroups_GroupId",
                table: "Babies");

            migrationBuilder.DropIndex(
                name: "IX_Babies_GroupId",
                table: "Babies");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Babies");
        }
    }
}

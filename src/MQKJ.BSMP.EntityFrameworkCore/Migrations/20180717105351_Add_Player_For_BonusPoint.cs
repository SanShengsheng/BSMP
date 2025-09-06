using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Add_Player_For_BonusPoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BonusPoints_PlayerId",
                table: "BonusPoints",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BonusPoints_Players_PlayerId",
                table: "BonusPoints",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BonusPoints_Players_PlayerId",
                table: "BonusPoints");

            migrationBuilder.DropIndex(
                name: "IX_BonusPoints_PlayerId",
                table: "BonusPoints");
        }
    }
}

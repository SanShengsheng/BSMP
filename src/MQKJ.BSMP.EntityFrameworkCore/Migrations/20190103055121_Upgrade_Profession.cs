using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_Profession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Professions_RewardId",
                table: "Professions",
                column: "RewardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Professions_Rewards_RewardId",
                table: "Professions",
                column: "RewardId",
                principalTable: "Rewards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professions_Rewards_RewardId",
                table: "Professions");

            migrationBuilder.DropIndex(
                name: "IX_Professions_RewardId",
                table: "Professions");
        }
    }
}

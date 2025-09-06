using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addbabyEventOptionrewardandconsumefiled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConsumeId",
                table: "BabyEventOptions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RewardId",
                table: "BabyEventOptions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BabyEventOptions_ConsumeId",
                table: "BabyEventOptions",
                column: "ConsumeId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyEventOptions_RewardId",
                table: "BabyEventOptions",
                column: "RewardId");

            migrationBuilder.AddForeignKey(
                name: "FK_BabyEventOptions_Rewards_ConsumeId",
                table: "BabyEventOptions",
                column: "ConsumeId",
                principalTable: "Rewards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BabyEventOptions_Rewards_RewardId",
                table: "BabyEventOptions",
                column: "RewardId",
                principalTable: "Rewards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyEventOptions_Rewards_ConsumeId",
                table: "BabyEventOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_BabyEventOptions_Rewards_RewardId",
                table: "BabyEventOptions");

            migrationBuilder.DropIndex(
                name: "IX_BabyEventOptions_ConsumeId",
                table: "BabyEventOptions");

            migrationBuilder.DropIndex(
                name: "IX_BabyEventOptions_RewardId",
                table: "BabyEventOptions");

            migrationBuilder.DropColumn(
                name: "ConsumeId",
                table: "BabyEventOptions");

            migrationBuilder.DropColumn(
                name: "RewardId",
                table: "BabyEventOptions");
        }
    }
}

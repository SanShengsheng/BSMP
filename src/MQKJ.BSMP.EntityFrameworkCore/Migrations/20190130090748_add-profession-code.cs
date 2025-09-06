using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addprofessioncode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "Professions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PlayerId",
                table: "Orders",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Players_PlayerId",
                table: "Orders",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Players_PlayerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PlayerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Professions");
        }
    }
}

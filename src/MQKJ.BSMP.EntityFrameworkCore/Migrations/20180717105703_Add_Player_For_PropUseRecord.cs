using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Add_Player_For_PropUseRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PropUseRecords_PlayerId",
                table: "PropUseRecords",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropUseRecords_Players_PlayerId",
                table: "PropUseRecords",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropUseRecords_Players_PlayerId",
                table: "PropUseRecords");

            migrationBuilder.DropIndex(
                name: "IX_PropUseRecords_PlayerId",
                table: "PropUseRecords");
        }
    }
}

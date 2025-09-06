using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addquestionforeignkeydefaultimg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Questions_DefaultImgId",
                table: "Questions",
                column: "DefaultImgId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_BSMPFiles_DefaultImgId",
                table: "Questions",
                column: "DefaultImgId",
                principalTable: "BSMPFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_BSMPFiles_DefaultImgId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_DefaultImgId",
                table: "Questions");
        }
    }
}

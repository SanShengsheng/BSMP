using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addfamilyandbabyrelateforeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Families_FatherId",
                table: "Families",
                column: "FatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Families_MotherId",
                table: "Families",
                column: "MotherId");

            migrationBuilder.CreateIndex(
                name: "IX_Babies_FamilyId",
                table: "Babies",
                column: "FamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Babies_Families_FamilyId",
                table: "Babies",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Families_Players_FatherId",
                table: "Families",
                column: "FatherId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Families_Players_MotherId",
                table: "Families",
                column: "MotherId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Babies_Families_FamilyId",
                table: "Babies");

            migrationBuilder.DropForeignKey(
                name: "FK_Families_Players_FatherId",
                table: "Families");

            migrationBuilder.DropForeignKey(
                name: "FK_Families_Players_MotherId",
                table: "Families");

            migrationBuilder.DropIndex(
                name: "IX_Families_FatherId",
                table: "Families");

            migrationBuilder.DropIndex(
                name: "IX_Families_MotherId",
                table: "Families");

            migrationBuilder.DropIndex(
                name: "IX_Babies_FamilyId",
                table: "Babies");
        }
    }
}

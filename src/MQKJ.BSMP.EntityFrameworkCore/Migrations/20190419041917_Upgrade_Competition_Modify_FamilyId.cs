using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_Competition_Modify_FamilyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Competitions_FamilyId",
                table: "Competitions",
                column: "FamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitions_Families_FamilyId",
                table: "Competitions",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_Families_FamilyId",
                table: "Competitions");

            migrationBuilder.DropIndex(
                name: "IX_Competitions_FamilyId",
                table: "Competitions");
        }
    }
}

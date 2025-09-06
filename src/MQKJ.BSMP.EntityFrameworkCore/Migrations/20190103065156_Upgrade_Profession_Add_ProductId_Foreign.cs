using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_Profession_Add_ProductId_Foreign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Professions_ProductId",
                table: "Professions",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Professions_Products_ProductId",
                table: "Professions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professions_Products_ProductId",
                table: "Professions");

            migrationBuilder.DropIndex(
                name: "IX_Professions_ProductId",
                table: "Professions");
        }
    }
}

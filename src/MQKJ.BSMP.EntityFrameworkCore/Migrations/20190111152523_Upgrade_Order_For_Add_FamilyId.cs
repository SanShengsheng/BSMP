using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_Order_For_Add_FamilyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FamilyId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FamilyId",
                table: "Orders",
                column: "FamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Families_FamilyId",
                table: "Orders",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Families_FamilyId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_FamilyId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FamilyId",
                table: "Orders");
        }
    }
}

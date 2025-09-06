using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class modify_abpuser_addfield_company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_CompanyId",
                table: "AbpUsers",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_Companies_CompanyId",
                table: "AbpUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_Companies_CompanyId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_CompanyId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AbpUsers");
        }
    }
}

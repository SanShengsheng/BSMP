using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addcompanyIdfieldinincomeandordertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "IncomeRecords",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "IncomeRecords");
        }
    }
}

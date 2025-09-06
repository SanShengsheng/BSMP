using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrde_BabyGrowUpRecords_cms_to_dbo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "BabyGrowUpRecords",
                schema: "CMS",
                newName: "BabyGrowUpRecords",
                newSchema: "dbo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CMS");

            migrationBuilder.RenameTable(
                name: "BabyGrowUpRecords",
                schema: "dbo",
                newName: "BabyGrowUpRecords",
                newSchema: "CMS");
        }
    }
}

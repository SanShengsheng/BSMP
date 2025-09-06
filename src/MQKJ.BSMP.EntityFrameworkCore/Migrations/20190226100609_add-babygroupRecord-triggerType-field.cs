using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addbabygroupRecordtriggerTypefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TriggerType",
                schema: "dbo",
                table: "BabyGrowUpRecords",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TriggerType",
                schema: "dbo",
                table: "BabyGrowUpRecords");
        }
    }
}

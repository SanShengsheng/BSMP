using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addfamilyandmqagentTotalsubsidyAmountfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubsidyAmount",
                table: "Families",
                newName: "TotalSubsidyAmount");

            migrationBuilder.AddColumn<double>(
                name: "TotalSubsidyAmount",
                table: "MqAgents",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalSubsidyAmount",
                table: "MqAgents");

            migrationBuilder.RenameColumn(
                name: "TotalSubsidyAmount",
                table: "Families",
                newName: "SubsidyAmount");
        }
    }
}

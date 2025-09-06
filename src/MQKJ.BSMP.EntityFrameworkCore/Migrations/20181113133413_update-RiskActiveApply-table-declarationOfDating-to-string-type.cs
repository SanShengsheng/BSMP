using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class updateRiskActiveApplytabledeclarationOfDatingtostringtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DeclarationOfDating",
                table: "RiskActiveApplys",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DeclarationOfDating",
                table: "RiskActiveApplys",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

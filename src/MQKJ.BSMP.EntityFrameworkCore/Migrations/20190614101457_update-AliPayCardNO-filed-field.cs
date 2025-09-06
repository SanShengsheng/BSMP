using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class updateAliPayCardNOfiledfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AliPayCardNO",
                table: "MqAgents",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AliPayCardNO",
                table: "MqAgents",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

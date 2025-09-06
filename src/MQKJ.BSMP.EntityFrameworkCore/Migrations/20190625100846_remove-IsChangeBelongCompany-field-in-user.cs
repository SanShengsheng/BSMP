using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class removeIsChangeBelongCompanyfieldinuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsChangeBelongCompany",
                table: "AbpUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsChangeBelongCompany",
                table: "AbpUsers",
                nullable: false,
                defaultValue: false);
        }
    }
}

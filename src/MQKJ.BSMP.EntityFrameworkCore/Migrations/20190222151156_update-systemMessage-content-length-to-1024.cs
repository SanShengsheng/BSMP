using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class updatesystemMessagecontentlengthto1024 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "SystemMessages",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 65,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "SystemMessages",
                maxLength: 65,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true);
        }
    }
}

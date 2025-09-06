using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class alterquestiondefaultimgidtocannullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "DefaultImgId",
                table: "Questions",
                nullable: true,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "DefaultImgId",
                table: "Questions",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}

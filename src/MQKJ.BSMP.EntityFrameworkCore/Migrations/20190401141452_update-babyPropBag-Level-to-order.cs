using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class updatebabyPropBagLeveltoorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextPropBagId",
                table: "BabyPropBags");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "BabyPropBags",
                newName: "Order");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Order",
                table: "BabyPropBags",
                newName: "Level");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "NextPropBagId",
                table: "BabyPropBags",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}

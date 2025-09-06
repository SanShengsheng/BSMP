using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class updatebabyPropBagisDisabledtoIsDisabled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isDisabled",
                table: "BabyPropBags",
                newName: "IsDisabled");

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "EndTime",
            //    table: "RunHorseInformations",
            //    nullable: true,
            //    defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
            //    oldClrType: typeof(DateTime),
            //    oldNullable: true,
            //    oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "BabyPropBags",
                newName: "isDisabled");

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "EndTime",
            //    table: "RunHorseInformations",
            //    nullable: true,
            //    defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
            //    oldClrType: typeof(DateTime),
            //    oldNullable: true,
            //    oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addbabyPropBagAndBabyProp_countfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "EndTime",
            //    table: "RunHorseInformations",
            //    nullable: true,
            //    defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
            //    oldClrType: typeof(DateTime),
            //    oldNullable: true,
            //    oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "BabyPropBagAndBabyProps",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "BabyPropBagAndBabyProps");

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

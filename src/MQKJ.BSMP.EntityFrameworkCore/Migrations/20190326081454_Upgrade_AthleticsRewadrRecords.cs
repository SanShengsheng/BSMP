using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_AthleticsRewadrRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxRanking",
                table: "AthleticsRewards");

            migrationBuilder.RenameColumn(
                name: "MinRanking",
                table: "AthleticsRewards",
                newName: "RankingNumber");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(2119, 3, 26, 16, 14, 52, 572, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2119, 3, 26, 13, 58, 25, 386, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RankingNumber",
                table: "AthleticsRewards",
                newName: "MinRanking");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "RunHorseInformations",
                nullable: true,
                defaultValue: new DateTime(2119, 3, 26, 13, 58, 25, 386, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2119, 3, 26, 16, 14, 52, 572, DateTimeKind.Local));

            migrationBuilder.AddColumn<int>(
                name: "MaxRanking",
                table: "AthleticsRewards",
                nullable: false,
                defaultValue: 0);
        }
    }
}

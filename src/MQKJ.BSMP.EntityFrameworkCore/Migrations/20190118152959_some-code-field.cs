using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class somecodefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnding",
                table: "BabyEvents");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "BabyEvents");

            migrationBuilder.RenameColumn(
                name: "GameTime",
                table: "BabyEvents",
                newName: "Wage");

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "Rewards",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "EventGroups",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "BabyEvents",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PreEventId",
                table: "BabyEvents",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "BabyEventOptions",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreEventId",
                table: "BabyEvents");

            migrationBuilder.RenameColumn(
                name: "Wage",
                table: "BabyEvents",
                newName: "GameTime");

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "Rewards",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "EventGroups",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "BabyEvents",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnding",
                table: "BabyEvents",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "BabyEvents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "BabyEventOptions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addbabytablesomefiled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CMS");

            migrationBuilder.RenameTable(
                name: "BabyGrowUpRecords",
                newName: "BabyGrowUpRecords",
                newSchema: "CMS");

            migrationBuilder.AlterColumn<double>(
                name: "BirthWeight",
                table: "Babies",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "BirthLength",
                table: "Babies",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ChildBirthOrder",
                table: "Babies",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChildBirthOrder",
                table: "Babies");

            migrationBuilder.RenameTable(
                name: "BabyGrowUpRecords",
                schema: "CMS",
                newName: "BabyGrowUpRecords");

            migrationBuilder.AlterColumn<int>(
                name: "BirthWeight",
                table: "Babies",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "BirthLength",
                table: "Babies",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}

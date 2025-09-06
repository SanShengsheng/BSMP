using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Add_FileName_And_NewFileName_And_RemoveFileExtensionNameForBSMPFile_To_BSMPDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileExtensionName",
                table: "BSMPFiles");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "BSMPFiles",
                type: "VARCHAR(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewFileName",
                table: "BSMPFiles",
                type: "VARCHAR(255)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "BSMPFiles");

            migrationBuilder.DropColumn(
                name: "NewFileName",
                table: "BSMPFiles");

            migrationBuilder.AddColumn<string>(
                name: "FileExtensionName",
                table: "BSMPFiles",
                type: "VARCHAR(20)",
                nullable: true);
        }
    }
}

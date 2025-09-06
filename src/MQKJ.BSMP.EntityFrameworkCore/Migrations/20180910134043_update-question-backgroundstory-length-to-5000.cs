using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class updatequestionbackgroundstorylengthto5000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BackgroundStoryMale",
                table: "Questions",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<string>(
                name: "BackgroundStoryFemale",
                table: "Questions",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 120);

            //migrationBuilder.AddColumn<int>(
            //    name: "ModifyCount",
            //    table: "Players",
            //    nullable: false,
            //    defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "ModifyCount",
            //    table: "Players");

            migrationBuilder.AlterColumn<string>(
                name: "BackgroundStoryMale",
                table: "Questions",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5000);

            migrationBuilder.AlterColumn<string>(
                name: "BackgroundStoryFemale",
                table: "Questions",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5000);
        }
    }
}

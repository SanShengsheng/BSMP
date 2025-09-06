using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class updatesensitiveWordlengthto1024 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "SensitiveWords",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 65,
                oldNullable: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsLoadBirthMovieMother",
            //    table: "Babies",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsWatchBirthMovieFather",
            //    table: "Babies",
            //    nullable: false,
            //    defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "IsLoadBirthMovieMother",
            //    table: "Babies");

            //migrationBuilder.DropColumn(
            //    name: "IsWatchBirthMovieFather",
            //    table: "Babies");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "SensitiveWords",
                maxLength: 65,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true);
        }
    }
}

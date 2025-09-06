using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgraded_Player_To_BSMP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WeChatName",
                table: "Players",
                newName: "NickName");

            migrationBuilder.RenameColumn(
                name: "IconImageId",
                table: "Players",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "AgeRangeMin",
                table: "Players",
                newName: "HeadUrl");

            migrationBuilder.RenameColumn(
                name: "AgeRangeMax",
                table: "Players",
                newName: "AgeRange");

            migrationBuilder.AddColumn<string>(
                name: "OpenId",
                table: "Players",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpenId",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Players",
                newName: "IconImageId");

            migrationBuilder.RenameColumn(
                name: "NickName",
                table: "Players",
                newName: "WeChatName");

            migrationBuilder.RenameColumn(
                name: "HeadUrl",
                table: "Players",
                newName: "AgeRangeMin");

            migrationBuilder.RenameColumn(
                name: "AgeRange",
                table: "Players",
                newName: "AgeRangeMax");
        }
    }
}

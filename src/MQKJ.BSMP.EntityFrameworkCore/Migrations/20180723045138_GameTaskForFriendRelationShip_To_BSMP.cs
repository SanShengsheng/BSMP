using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class GameTaskForFriendRelationShip_To_BSMP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FriendRelationship",
                table: "GameTasks");

            migrationBuilder.AddColumn<int>(
                name: "RelationDegree",
                table: "GameTasks",
                maxLength: 30,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelationDegree",
                table: "GameTasks");

            migrationBuilder.AddColumn<string>(
                name: "FriendRelationship",
                table: "GameTasks",
                maxLength: 30,
                nullable: true);
        }
    }
}

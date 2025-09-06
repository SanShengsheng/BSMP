using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_GameTask_Add_InviteeAppointmentContent_Remove_ConnectionId_BeConnectionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeConnectionId",
                table: "GameTasks");

            migrationBuilder.DropColumn(
                name: "ConnectionId",
                table: "GameTasks");

            migrationBuilder.AddColumn<string>(
                name: "InviteeAppointmentContent",
                table: "GameTasks",
                maxLength: 300,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InviteeAppointmentContent",
                table: "GameTasks");

            migrationBuilder.AddColumn<string>(
                name: "BeConnectionId",
                table: "GameTasks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConnectionId",
                table: "GameTasks",
                nullable: true);
        }
    }
}

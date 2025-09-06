using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addExpirationGroupIdandorderfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpirationGroupId",
                table: "BabyEvents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "BabyEvents",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationGroupId",
                table: "BabyEvents");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "BabyEvents");
        }
    }
}

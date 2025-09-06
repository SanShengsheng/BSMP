using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addinformationremrkandbabyEventIdfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BabyEventId",
                table: "Informations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Informations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Informations_BabyEventId",
                table: "Informations",
                column: "BabyEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Informations_BabyEvents_BabyEventId",
                table: "Informations",
                column: "BabyEventId",
                principalTable: "BabyEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Informations_BabyEvents_BabyEventId",
                table: "Informations");

            migrationBuilder.DropIndex(
                name: "IX_Informations_BabyEventId",
                table: "Informations");

            migrationBuilder.DropColumn(
                name: "BabyEventId",
                table: "Informations");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Informations");
        }
    }
}

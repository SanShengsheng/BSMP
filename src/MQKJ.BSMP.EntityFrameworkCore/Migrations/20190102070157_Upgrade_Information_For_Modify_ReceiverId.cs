using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_Information_For_Modify_ReceiverId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Informations_Players_ReceiverId",
                table: "Informations");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReceiverId",
                table: "Informations",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Informations_Players_ReceiverId",
                table: "Informations",
                column: "ReceiverId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Informations_Players_ReceiverId",
                table: "Informations");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReceiverId",
                table: "Informations",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Informations_Players_ReceiverId",
                table: "Informations",
                column: "ReceiverId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

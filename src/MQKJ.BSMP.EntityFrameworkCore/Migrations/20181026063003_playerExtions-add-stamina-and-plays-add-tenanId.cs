using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class playerExtionsaddstaminaandplaysaddtenanId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppId",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "Stamina",
                table: "Players",
                newName: "TenantId");

            migrationBuilder.RenameColumn(
                name: "MiniProgrammCode",
                table: "Players",
                newName: "PlayerExtensionId");

            migrationBuilder.AddColumn<int>(
                name: "Stamina",
                table: "PlayerExtensions",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Players_PlayerExtensionId",
            //    table: "Players",
            //    column: "PlayerExtensionId"
            //   );

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Players_PlayerExtensions_PlayerExtensionId",
            //    table: "Players",
            //    column: "PlayerExtensionId",
            //    principalTable: "PlayerExtensions",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Players_PlayerExtensions_PlayerExtensionId",
            //    table: "Players");

            //migrationBuilder.DropIndex(
            //    name: "IX_Players_PlayerExtensionId",
            //    table: "Players");

            migrationBuilder.DropColumn(
                name: "Stamina",
                table: "PlayerExtensions");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "Players",
                newName: "Stamina");

            migrationBuilder.RenameColumn(
                name: "PlayerExtensionId",
                table: "Players",
                newName: "MiniProgrammCode");

            migrationBuilder.AddColumn<string>(
                name: "AppId",
                table: "Players",
                nullable: true);
        }
    }
}

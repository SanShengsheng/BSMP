using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_LoveCardFile_Add_LoveCardId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoveCardFiles_Players_PlayerId",
                table: "LoveCardFiles");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "LoveCardFiles",
                newName: "LoveCardId");

            migrationBuilder.RenameIndex(
                name: "IX_LoveCardFiles_PlayerId",
                table: "LoveCardFiles",
                newName: "IX_LoveCardFiles_LoveCardId");

            migrationBuilder.CreateTable(
                name: "LoveCardOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LoveCardId = table.Column<Guid>(nullable: false),
                    LoveCardOptionType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoveCardOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoveCardOptions_LoveCards_LoveCardId",
                        column: x => x.LoveCardId,
                        principalTable: "LoveCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoveCardOptions_LoveCardId",
                table: "LoveCardOptions",
                column: "LoveCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoveCardFiles_LoveCards_LoveCardId",
                table: "LoveCardFiles",
                column: "LoveCardId",
                principalTable: "LoveCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoveCardFiles_LoveCards_LoveCardId",
                table: "LoveCardFiles");

            migrationBuilder.DropTable(
                name: "LoveCardOptions");

            migrationBuilder.RenameColumn(
                name: "LoveCardId",
                table: "LoveCardFiles",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_LoveCardFiles_LoveCardId",
                table: "LoveCardFiles",
                newName: "IX_LoveCardFiles_PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoveCardFiles_Players_PlayerId",
                table: "LoveCardFiles",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}

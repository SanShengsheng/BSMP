using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Rmove_LoveCardOptions_From_BSMP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoveCardOptions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoveCardOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BeOptionPlayerId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LoveCardOptionType = table.Column<int>(nullable: false),
                    OptionPlayerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoveCardOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoveCardOptions_Players_OptionPlayerId",
                        column: x => x.OptionPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoveCardOptions_OptionPlayerId",
                table: "LoveCardOptions",
                column: "OptionPlayerId");
        }
    }
}

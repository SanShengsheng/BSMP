using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class removeplayerExtensiontable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Players_PlayerExtensions_PlayerExtensionId",
            //    table: "Players");

            migrationBuilder.DropTable(
                name: "PlayerExtensions");

            //migrationBuilder.DropIndex(
            //    name: "IX_Players_PlayerExtensionId",
            //    table: "Players");

            migrationBuilder.DropColumn(
                name: "PlayerExtensionId",
                table: "Players");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerExtensionId",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PlayerExtensions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    ExtensionFiled1 = table.Column<int>(nullable: false),
                    ExtensionFiled2 = table.Column<int>(nullable: false),
                    ExtensionFiled3 = table.Column<int>(nullable: false),
                    ExtensionFiled4 = table.Column<string>(nullable: true),
                    ExtensionFiled5 = table.Column<string>(nullable: true),
                    ExtensionFiled6 = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LoveScore = table.Column<int>(nullable: false),
                    PlayerGuid = table.Column<Guid>(nullable: false),
                    Stamina = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerExtensions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_PlayerExtensionId",
                table: "Players",
                column: "PlayerExtensionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_PlayerExtensions_PlayerExtensionId",
                table: "Players",
                column: "PlayerExtensionId",
                principalTable: "PlayerExtensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

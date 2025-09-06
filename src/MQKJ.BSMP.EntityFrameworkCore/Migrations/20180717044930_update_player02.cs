using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class update_player02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerExtensions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    PlayerGuid = table.Column<Guid>(nullable: false),
                    LoveScore = table.Column<int>(nullable: false),
                    ExtensionFiled1 = table.Column<int>(nullable: false),
                    ExtensionFiled2 = table.Column<int>(nullable: false),
                    ExtensionFiled3 = table.Column<int>(nullable: false),
                    ExtensionFiled4 = table.Column<string>(nullable: true),
                    ExtensionFiled5 = table.Column<string>(nullable: true),
                    ExtensionFiled6 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerExtensions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    PlayerGuid = table.Column<Guid>(nullable: false),
                    WeChatName = table.Column<string>(nullable: false),
                    IconImageId = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    AgeRangeMin = table.Column<int>(nullable: false),
                    AgeRangeMax = table.Column<int>(nullable: false),
                    AuthorizeDateTime = table.Column<DateTime>(nullable: false),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerGuid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerExtensions");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}

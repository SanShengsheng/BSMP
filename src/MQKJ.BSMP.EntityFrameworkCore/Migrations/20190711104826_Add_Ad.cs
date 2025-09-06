using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Add_Ad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adviertisements",
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
                    Name = table.Column<string>(nullable: true),
                    BannerImagePath = table.Column<string>(nullable: true),
                    InsertImagePath = table.Column<string>(nullable: true),
                    FixedImagePath = table.Column<string>(nullable: true),
                    WordLink = table.Column<string>(nullable: true),
                    ExpandPath = table.Column<string>(nullable: true),
                    MinPorgramLogo = table.Column<string>(nullable: true),
                    OrderNumber = table.Column<string>(nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    AdviertisementState = table.Column<int>(nullable: false),
                    AdviertisementPlatform = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adviertisements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adviertisements_AbpTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "AbpTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerAdviertisements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: false),
                    AdviertisementId = table.Column<int>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    UUID = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerAdviertisements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerAdviertisements_Adviertisements_AdviertisementId",
                        column: x => x.AdviertisementId,
                        principalTable: "Adviertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerAdviertisements_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adviertisements_TenantId",
                table: "Adviertisements",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAdviertisements_AdviertisementId",
                table: "PlayerAdviertisements",
                column: "AdviertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAdviertisements_PlayerId",
                table: "PlayerAdviertisements",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerAdviertisements");

            migrationBuilder.DropTable(
                name: "Adviertisements");
        }
    }
}

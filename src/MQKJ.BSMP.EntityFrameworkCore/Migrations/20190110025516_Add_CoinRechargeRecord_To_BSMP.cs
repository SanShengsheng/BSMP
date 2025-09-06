using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Add_CoinRechargeRecord_To_BSMP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoinRechargeRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    RechargeCount = table.Column<int>(nullable: false),
                    RechargerId = table.Column<Guid>(nullable: false),
                    FamilyId = table.Column<int>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: true),
                    SourceType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinRechargeRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoinRechargeRecords_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoinRechargeRecords_Players_RechargerId",
                        column: x => x.RechargerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoinRechargeRecords_FamilyId",
                table: "CoinRechargeRecords",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinRechargeRecords_RechargerId",
                table: "CoinRechargeRecords",
                column: "RechargerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoinRechargeRecords");
        }
    }
}

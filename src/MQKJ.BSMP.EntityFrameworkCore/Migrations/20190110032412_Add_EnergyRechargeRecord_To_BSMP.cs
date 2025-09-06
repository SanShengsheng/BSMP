using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Add_EnergyRechargeRecord_To_BSMP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnergyRechargeRecords",
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
                    CoinAmount = table.Column<int>(nullable: false),
                    EnergyCount = table.Column<int>(nullable: false),
                    RechargerId = table.Column<Guid>(nullable: true),
                    BabyId = table.Column<int>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: true),
                    SourceType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyRechargeRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergyRechargeRecords_Babies_BabyId",
                        column: x => x.BabyId,
                        principalTable: "Babies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnergyRechargeRecords_Players_RechargerId",
                        column: x => x.RechargerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnergyRechargeRecords_BabyId",
                table: "EnergyRechargeRecords",
                column: "BabyId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergyRechargeRecords_RechargerId",
                table: "EnergyRechargeRecords",
                column: "RechargerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnergyRechargeRecords");
        }
    }
}

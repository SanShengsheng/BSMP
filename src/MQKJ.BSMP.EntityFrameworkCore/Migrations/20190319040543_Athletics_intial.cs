using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Athletics_intial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AthleticsRewards",
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
                    FamilyId = table.Column<int>(nullable: false),
                    BabyId = table.Column<int>(nullable: false),
                    MaxRanking = table.Column<int>(nullable: false),
                    MinRanking = table.Column<int>(nullable: false),
                    ChineseBabyMarketId = table.Column<int>(nullable: false),
                    RewardType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AthleticsRewards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeasonManagements",
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
                    SeasonNumber = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    IsCurrent = table.Column<bool>(nullable: false),
                    RankingShowPlayerCount = table.Column<int>(nullable: false),
                    CanPKCount = table.Column<int>(nullable: false),
                    MotherDefaultFightCount = table.Column<int>(nullable: false),
                    FatherDefaultFightCount = table.Column<int>(nullable: false),
                    MaxFightCount = table.Column<int>(nullable: false),
                    FightCountPrice = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeasonManagements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuyFightCountRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PurchaserId = table.Column<Guid>(nullable: true),
                    FamilyId = table.Column<int>(nullable: true),
                    FightCount = table.Column<int>(nullable: false),
                    CoinCount = table.Column<double>(nullable: false),
                    SeasonManagementId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    BabyId = table.Column<int>(nullable: false),
                    SourceType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyFightCountRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyFightCountRecords_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BuyFightCountRecords_Players_PurchaserId",
                        column: x => x.PurchaserId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BuyFightCountRecords_SeasonManagements_SeasonManagementId",
                        column: x => x.SeasonManagementId,
                        principalTable: "SeasonManagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FamilyId = table.Column<int>(nullable: false),
                    BabyId = table.Column<int>(nullable: false),
                    SeasonManagementId = table.Column<int>(nullable: false),
                    FatherFightCount = table.Column<int>(nullable: false),
                    MotherFightCount = table.Column<int>(nullable: false),
                    WiningCount = table.Column<int>(nullable: false),
                    FailedCount = table.Column<int>(nullable: false),
                    RankingNumber = table.Column<int>(nullable: false),
                    GamePoint = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Competitions_Babies_BabyId",
                        column: x => x.BabyId,
                        principalTable: "Babies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Competitions_SeasonManagements_SeasonManagementId",
                        column: x => x.SeasonManagementId,
                        principalTable: "SeasonManagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FightRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FamilyId = table.Column<int>(nullable: true),
                    InitiatorId = table.Column<Guid>(nullable: false),
                    InitiatorBabyId = table.Column<int>(nullable: false),
                    OtherBabyId = table.Column<int>(nullable: false),
                    WinningRatio = table.Column<double>(nullable: false),
                    RandomNumber = table.Column<double>(nullable: false),
                    BabyAttributeCode = table.Column<int>(nullable: false),
                    SeasonManagementId = table.Column<int>(nullable: false),
                    GamePoint = table.Column<int>(nullable: false),
                    LastTimePoint = table.Column<int>(nullable: false),
                    CurrentPoint = table.Column<int>(nullable: false),
                    LastTimeRanking = table.Column<int>(nullable: false),
                    CurrentRanking = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    FightResultEnum = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FightRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FightRecords_Babies_InitiatorBabyId",
                        column: x => x.InitiatorBabyId,
                        principalTable: "Babies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FightRecords_Players_InitiatorId",
                        column: x => x.InitiatorId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FightRecords_Babies_OtherBabyId",
                        column: x => x.OtherBabyId,
                        principalTable: "Babies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FightRecords_SeasonManagements_SeasonManagementId",
                        column: x => x.SeasonManagementId,
                        principalTable: "SeasonManagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuyFightCountRecords_FamilyId",
                table: "BuyFightCountRecords",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyFightCountRecords_PurchaserId",
                table: "BuyFightCountRecords",
                column: "PurchaserId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyFightCountRecords_SeasonManagementId",
                table: "BuyFightCountRecords",
                column: "SeasonManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_BabyId",
                table: "Competitions",
                column: "BabyId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_SeasonManagementId",
                table: "Competitions",
                column: "SeasonManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_FightRecords_InitiatorBabyId",
                table: "FightRecords",
                column: "InitiatorBabyId");

            migrationBuilder.CreateIndex(
                name: "IX_FightRecords_InitiatorId",
                table: "FightRecords",
                column: "InitiatorId");

            migrationBuilder.CreateIndex(
                name: "IX_FightRecords_OtherBabyId",
                table: "FightRecords",
                column: "OtherBabyId");

            migrationBuilder.CreateIndex(
                name: "IX_FightRecords_SeasonManagementId",
                table: "FightRecords",
                column: "SeasonManagementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AthleticsRewards");

            migrationBuilder.DropTable(
                name: "BuyFightCountRecords");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropTable(
                name: "FightRecords");

            migrationBuilder.DropTable(
                name: "SeasonManagements");
        }
    }
}

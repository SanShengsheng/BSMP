using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Add_AthleticsInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentRanking",
                table: "FightRecords");

            migrationBuilder.DropColumn(
                name: "LastTimeRanking",
                table: "FightRecords");

            migrationBuilder.DropColumn(
                name: "RankingNumber",
                table: "Competitions");

            migrationBuilder.CreateTable(
                name: "AthleticsInformations",
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
                    ReceiverId = table.Column<Guid>(nullable: true),
                    FamilyId = table.Column<int>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    AthleticsInformationType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AthleticsInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AthleticsInformations_Players_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AthleticsInformations_ReceiverId",
                table: "AthleticsInformations",
                column: "ReceiverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AthleticsInformations");

            migrationBuilder.AddColumn<int>(
                name: "CurrentRanking",
                table: "FightRecords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastTimeRanking",
                table: "FightRecords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RankingNumber",
                table: "Competitions",
                nullable: false,
                defaultValue: 0);
        }
    }
}

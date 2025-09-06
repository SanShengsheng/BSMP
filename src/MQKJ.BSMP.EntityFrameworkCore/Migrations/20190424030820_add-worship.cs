using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addworship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Prestiges",
                table: "Families",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FamilyWorshipRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FromFamilyId = table.Column<int>(nullable: false),
                    ToFamilyId = table.Column<int>(nullable: false),
                    Prestiges = table.Column<int>(nullable: false),
                    Coins = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyWorshipRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FamilyWorshipRewards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RankMin = table.Column<int>(nullable: false),
                    RankMax = table.Column<int>(nullable: false),
                    CoinsMin = table.Column<int>(nullable: false),
                    CoinsMax = table.Column<int>(nullable: false),
                    Prestiges = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyWorshipRewards", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FamilyWorshipRecords");

            migrationBuilder.DropTable(
                name: "FamilyWorshipRewards");

            migrationBuilder.DropColumn(
                name: "Prestiges",
                table: "Families");
        }
    }
}

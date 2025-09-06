using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class add_table_for_autorunner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutoRunnerConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    FamilyId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    ProfressionId = table.Column<int>(nullable: true),
                    FamilyLevel = table.Column<int>(nullable: false),
                    ConsumeLevel = table.Column<int>(nullable: false),
                    StudyCount = table.Column<int>(nullable: false),
                    BabyProperty = table.Column<int>(nullable: true),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoRunnerConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AutoRunnerRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    FamilyId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    ActionType = table.Column<int>(nullable: false),
                    RelateionId = table.Column<string>(nullable: true),
                    OriginalData = table.Column<string>(nullable: true),
                    NewData = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoRunnerRecords", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutoRunnerConfigs");

            migrationBuilder.DropTable(
                name: "AutoRunnerRecords");
        }
    }
}

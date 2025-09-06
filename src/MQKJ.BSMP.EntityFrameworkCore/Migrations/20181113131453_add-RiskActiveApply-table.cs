using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addRiskActiveApplytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RiskActiveApplys",
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
                    Season = table.Column<int>(nullable: false),
                    NickName = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    BirthDateTime = table.Column<DateTime>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Hobbies = table.Column<string>(nullable: true),
                    SelfIntroduction = table.Column<string>(nullable: true),
                    DeclarationOfDating = table.Column<int>(nullable: false),
                    Source = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskActiveApplys", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RiskActiveApplys");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Upgrade_Order_For_Add_IsWithdrawCash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<Guid>(
            //    name: "PlayerId",
            //    schema: "dbo",
            //    table: "BabyGrowUpRecords",
            //    nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsWithdrawCash",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.CreateTable(
            //    name: "AutoRunnerConfigs",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        IsDeleted = table.Column<bool>(nullable: false),
            //        CreationTime = table.Column<DateTime>(nullable: false),
            //        FamilyId = table.Column<int>(nullable: false),
            //        PlayerId = table.Column<Guid>(nullable: false),
            //        GroupId = table.Column<int>(nullable: false),
            //        ProfressionId = table.Column<int>(nullable: true),
            //        FamilyLevel = table.Column<int>(nullable: false),
            //        ConsumeLevel = table.Column<int>(nullable: false),
            //        StudyCount = table.Column<int>(nullable: false),
            //        BabyProperty = table.Column<int>(nullable: true),
            //        State = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AutoRunnerConfigs", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_AutoRunnerConfigs_Families_FamilyId",
            //            column: x => x.FamilyId,
            //            principalTable: "Families",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_AutoRunnerConfigs_EventGroups_GroupId",
            //            column: x => x.GroupId,
            //            principalTable: "EventGroups",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_AutoRunnerConfigs_Players_PlayerId",
            //            column: x => x.PlayerId,
            //            principalTable: "Players",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_AutoRunnerConfigs_Professions_ProfressionId",
            //            column: x => x.ProfressionId,
            //            principalTable: "Professions",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AutoRunnerRecords",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false),
            //        IsDeleted = table.Column<bool>(nullable: false),
            //        CreationTime = table.Column<DateTime>(nullable: false),
            //        FamilyId = table.Column<int>(nullable: false),
            //        PlayerId = table.Column<Guid>(nullable: false),
            //        GroupId = table.Column<int>(nullable: false),
            //        ActionType = table.Column<int>(nullable: false),
            //        RelateionId = table.Column<string>(nullable: true),
            //        OriginalData = table.Column<string>(nullable: true),
            //        NewData = table.Column<string>(nullable: true),
            //        Description = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AutoRunnerRecords", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_AutoRunnerRecords_Families_FamilyId",
            //            column: x => x.FamilyId,
            //            principalTable: "Families",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_AutoRunnerRecords_EventGroups_GroupId",
            //            column: x => x.GroupId,
            //            principalTable: "EventGroups",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_AutoRunnerRecords_Players_PlayerId",
            //            column: x => x.PlayerId,
            //            principalTable: "Players",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_AutoRunnerConfigs_FamilyId",
            //    table: "AutoRunnerConfigs",
            //    column: "FamilyId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AutoRunnerConfigs_GroupId",
            //    table: "AutoRunnerConfigs",
            //    column: "GroupId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AutoRunnerConfigs_PlayerId",
            //    table: "AutoRunnerConfigs",
            //    column: "PlayerId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AutoRunnerConfigs_ProfressionId",
            //    table: "AutoRunnerConfigs",
            //    column: "ProfressionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AutoRunnerRecords_FamilyId",
            //    table: "AutoRunnerRecords",
            //    column: "FamilyId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AutoRunnerRecords_GroupId",
            //    table: "AutoRunnerRecords",
            //    column: "GroupId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AutoRunnerRecords_PlayerId",
            //    table: "AutoRunnerRecords",
            //    column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "AutoRunnerConfigs");

            //migrationBuilder.DropTable(
            //    name: "AutoRunnerRecords");

            //migrationBuilder.DropColumn(
            //    name: "PlayerId",
            //    schema: "dbo",
            //    table: "BabyGrowUpRecords");

            migrationBuilder.DropColumn(
                name: "IsWithdrawCash",
                table: "Orders");
        }
    }
}

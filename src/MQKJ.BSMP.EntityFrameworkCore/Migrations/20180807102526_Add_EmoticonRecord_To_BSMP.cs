using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Add_EmoticonRecord_To_BSMP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_BonusPoints_BonusPoinEvents_EventId",
            //    table: "BonusPoints");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_BonusPoints_Players_PlayerId",
            //    table: "BonusPoints");

            //migrationBuilder.DropTable(
            //    name: "BonusPoinEvents");

            //migrationBuilder.DropIndex(
            //    name: "IX_BonusPoints_EventId",
            //    table: "BonusPoints");

            //migrationBuilder.DropIndex(
            //    name: "IX_BonusPoints_PlayerId",
            //    table: "BonusPoints");

            //migrationBuilder.DropColumn(
            //    name: "EventId",
            //    table: "BonusPoints");

            //migrationBuilder.DropColumn(
            //    name: "PlayerId",
            //    table: "BonusPoints");

            //migrationBuilder.DropColumn(
            //    name: "UpdateTime",
            //    table: "BonusPoints");

            //migrationBuilder.RenameColumn(
            //    name: "GatherCount",
            //    table: "BonusPoints",
            //    newName: "PointsCount");

            //migrationBuilder.RenameColumn(
            //    name: "ExtensionFiled",
            //    table: "BonusPoints",
            //    newName: "EventName");

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "BonusPoints",
            //    nullable: false,
            //    oldClrType: typeof(Guid))
            //    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            //migrationBuilder.AddColumn<string>(
            //    name: "Code",
            //    table: "BonusPoints",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "EventDescription",
            //    table: "BonusPoints",
            //    nullable: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsApplyWechat",
            //    table: "AbpTenants",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<string>(
            //    name: "WechatAppId",
            //    table: "AbpTenants",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "WechatAppSecret",
            //    table: "AbpTenants",
            //    nullable: true);

            //migrationBuilder.CreateTable(
            //    name: "BonusPointRecords",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false),
            //        CreationTime = table.Column<DateTime>(nullable: false),
            //        CreatorUserId = table.Column<long>(nullable: true),
            //        LastModificationTime = table.Column<DateTime>(nullable: true),
            //        LastModifierUserId = table.Column<long>(nullable: true),
            //        IsDeleted = table.Column<bool>(nullable: false),
            //        DeleterUserId = table.Column<long>(nullable: true),
            //        DeletionTime = table.Column<DateTime>(nullable: true),
            //        GatherCount = table.Column<int>(nullable: false),
            //        PlayerId = table.Column<Guid>(nullable: false),
            //        BonusPointId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_BonusPointRecords", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_BonusPointRecords_BonusPoints_BonusPointId",
            //            column: x => x.BonusPointId,
            //            principalTable: "BonusPoints",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_BonusPointRecords_Players_PlayerId",
            //            column: x => x.PlayerId,
            //            principalTable: "Players",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateTable(
                name: "EmoticonRecords",
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
                    Code = table.Column<string>(nullable: true),
                    PlayerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmoticonRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmoticonRecords_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_BonusPointRecords_BonusPointId",
            //    table: "BonusPointRecords",
            //    column: "BonusPointId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_BonusPointRecords_PlayerId",
            //    table: "BonusPointRecords",
            //    column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmoticonRecords_PlayerId",
                table: "EmoticonRecords",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "BonusPointRecords");

            migrationBuilder.DropTable(
                name: "EmoticonRecords");

            //migrationBuilder.DropColumn(
            //    name: "Code",
            //    table: "BonusPoints");

            //migrationBuilder.DropColumn(
            //    name: "EventDescription",
            //    table: "BonusPoints");

            //migrationBuilder.DropColumn(
            //    name: "IsApplyWechat",
            //    table: "AbpTenants");

            //migrationBuilder.DropColumn(
            //    name: "WechatAppId",
            //    table: "AbpTenants");

            //migrationBuilder.DropColumn(
            //    name: "WechatAppSecret",
            //    table: "AbpTenants");

            //migrationBuilder.RenameColumn(
            //    name: "PointsCount",
            //    table: "BonusPoints",
            //    newName: "GatherCount");

            //migrationBuilder.RenameColumn(
            //    name: "EventName",
            //    table: "BonusPoints",
            //    newName: "ExtensionFiled");

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "Id",
            //    table: "BonusPoints",
            //    nullable: false,
            //    oldClrType: typeof(int))
            //    .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            //migrationBuilder.AddColumn<Guid>(
            //    name: "EventId",
            //    table: "BonusPoints",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.AddColumn<Guid>(
            //    name: "PlayerId",
            //    table: "BonusPoints",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "UpdateTime",
            //    table: "BonusPoints",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            //migrationBuilder.CreateTable(
            //    name: "BonusPoinEvents",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false),
            //        CreationTime = table.Column<DateTime>(nullable: false),
            //        CreatorUserId = table.Column<long>(nullable: true),
            //        DeleterUserId = table.Column<long>(nullable: true),
            //        DeletionTime = table.Column<DateTime>(nullable: true),
            //        EventName = table.Column<string>(maxLength: 120, nullable: false),
            //        IsDeleted = table.Column<bool>(nullable: false),
            //        LastModificationTime = table.Column<DateTime>(nullable: true),
            //        LastModifierUserId = table.Column<long>(nullable: true),
            //        Sort = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_BonusPoinEvents", x => x.Id);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_BonusPoints_EventId",
            //    table: "BonusPoints",
            //    column: "EventId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_BonusPoints_PlayerId",
            //    table: "BonusPoints",
            //    column: "PlayerId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_BonusPoints_BonusPoinEvents_EventId",
            //    table: "BonusPoints",
            //    column: "EventId",
            //    principalTable: "BonusPoinEvents",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_BonusPoints_Players_PlayerId",
            //    table: "BonusPoints",
            //    column: "PlayerId",
            //    principalTable: "Players",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}

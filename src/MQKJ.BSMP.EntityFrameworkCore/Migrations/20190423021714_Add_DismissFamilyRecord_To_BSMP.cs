using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Add_DismissFamilyRecord_To_BSMP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FamilyState",
                table: "Families",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DismissFamilyRecords",
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
                    DismissFamilyType = table.Column<int>(nullable: false),
                    InitiatorId = table.Column<Guid>(nullable: true),
                    OrderId = table.Column<Guid>(nullable: true),
                    FamilyState = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DismissFamilyRecords", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DismissFamilyRecords");

            migrationBuilder.DropColumn(
                name: "FamilyState",
                table: "Families");
        }
    }
}

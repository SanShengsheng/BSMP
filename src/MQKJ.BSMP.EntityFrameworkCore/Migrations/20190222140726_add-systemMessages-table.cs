using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addsystemMessagestable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemMessages",
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
                    Content = table.Column<string>(maxLength: 65, nullable: true),
                    NoticeType = table.Column<int>(maxLength: 65, nullable: false),
                    PeriodType = table.Column<int>(maxLength: 65, nullable: false),
                    PriorityLevel = table.Column<int>(maxLength: 65, nullable: false),
                    ExprieDateTime = table.Column<DateTime>(maxLength: 65, nullable: false),
                    StartDateTime = table.Column<DateTime>(maxLength: 65, nullable: false),
                    Code = table.Column<int>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    Period = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemMessages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemMessages");
        }
    }
}

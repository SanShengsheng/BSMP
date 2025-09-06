using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addoperationActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RelaseLog",
                table: "VersionManages",
                newName: "ReleaseLog");

            migrationBuilder.CreateTable(
                name: "OperationActivities",
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
                    TenantId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    SubTitle = table.Column<string>(nullable: true),
                    IsPopup = table.Column<bool>(nullable: false),
                    ExpireDateTime = table.Column<DateTime>(nullable: true),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    Img = table.Column<string>(nullable: true),
                    ActivityType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationActivities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationActivities");

            migrationBuilder.RenameColumn(
                name: "ReleaseLog",
                table: "VersionManages",
                newName: "RelaseLog");
        }
    }
}

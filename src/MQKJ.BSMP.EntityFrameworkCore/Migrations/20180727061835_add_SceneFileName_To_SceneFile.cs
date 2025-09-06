using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class add_SceneFileName_To_SceneFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_SceneFiles_SceneId",
            //    table: "SceneFiles");

            //migrationBuilder.AddColumn<string>(
            //    name: "Code",
            //    table: "TagTypes",
            //    nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SceneFileName",
                table: "SceneFiles",
                nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_SceneFiles_SceneId",
            //    table: "SceneFiles",
            //    column: "SceneId",
            //    unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_SceneFiles_SceneId",
            //    table: "SceneFiles");

            //migrationBuilder.DropColumn(
            //    name: "Code",
            //    table: "TagTypes");

            migrationBuilder.DropColumn(
                name: "SceneFileName",
                table: "SceneFiles");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SceneFiles_SceneId",
            //    table: "SceneFiles",
            //    column: "SceneId");
        }
    }
}

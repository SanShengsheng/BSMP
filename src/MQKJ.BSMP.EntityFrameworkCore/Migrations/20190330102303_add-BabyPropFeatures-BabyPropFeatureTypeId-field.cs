using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addBabyPropFeaturesBabyPropFeatureTypeIdfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<Guid>(
                name: "BabyPropFeatureTypeId",
                table: "BabyPropFeatures",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BabyPropFeatures_BabyPropFeatureTypeId",
                table: "BabyPropFeatures",
                column: "BabyPropFeatureTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BabyPropFeatures_BabyPropFeatureTypes_BabyPropFeatureTypeId",
                table: "BabyPropFeatures",
                column: "BabyPropFeatureTypeId",
                principalTable: "BabyPropFeatureTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyPropFeatures_BabyPropFeatureTypes_BabyPropFeatureTypeId",
                table: "BabyPropFeatures");

            migrationBuilder.DropIndex(
                name: "IX_BabyPropFeatures_BabyPropFeatureTypeId",
                table: "BabyPropFeatures");

            migrationBuilder.DropColumn(
                name: "BabyPropFeatureTypeId",
                table: "BabyPropFeatures");

      
        }
    }
}

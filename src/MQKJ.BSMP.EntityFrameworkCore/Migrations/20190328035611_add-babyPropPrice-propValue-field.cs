using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addbabyPropPricepropValuefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PropValue",
                table: "BabyProps");

            migrationBuilder.AddColumn<double>(
                name: "PropValue",
                table: "BabyPropPrices",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PropValue",
                table: "BabyPropPrices");

            migrationBuilder.AddColumn<decimal>(
                name: "PropValue",
                table: "BabyProps",
                nullable: false,
                defaultValue: 0m);
        }
    }
}

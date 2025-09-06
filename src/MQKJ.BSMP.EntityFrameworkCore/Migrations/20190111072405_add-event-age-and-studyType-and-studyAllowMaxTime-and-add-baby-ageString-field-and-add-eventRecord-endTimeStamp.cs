using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addeventageandstudyTypeandstudyAllowMaxTimeandaddbabyageStringfieldandaddeventRecordendTimeStamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Age",
                table: "BabyEvents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudyAllowMaxTime",
                table: "BabyEvents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudyType",
                table: "BabyEvents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "EndTimeStamp",
                table: "BabyEventRecords",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AgeString",
                table: "Babies",
                nullable: true);

       
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.DropColumn(
                name: "Age",
                table: "BabyEvents");

            migrationBuilder.DropColumn(
                name: "StudyAllowMaxTime",
                table: "BabyEvents");

            migrationBuilder.DropColumn(
                name: "StudyType",
                table: "BabyEvents");

            migrationBuilder.DropColumn(
                name: "EndTimeStamp",
                table: "BabyEventRecords");

            migrationBuilder.DropColumn(
                name: "AgeString",
                table: "Babies");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class modify_familyworshiprecord_addfield_babyids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FromBabyId",
                table: "FamilyWorshipRecords",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToBabyId",
                table: "FamilyWorshipRecords",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FamilyWorshipRecords_FromBabyId",
                table: "FamilyWorshipRecords",
                column: "FromBabyId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyWorshipRecords_ToBabyId",
                table: "FamilyWorshipRecords",
                column: "ToBabyId");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyWorshipRecords_Babies_FromBabyId",
                table: "FamilyWorshipRecords",
                column: "FromBabyId",
                principalTable: "Babies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyWorshipRecords_Babies_ToBabyId",
                table: "FamilyWorshipRecords",
                column: "ToBabyId",
                principalTable: "Babies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyWorshipRecords_Babies_FromBabyId",
                table: "FamilyWorshipRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_FamilyWorshipRecords_Babies_ToBabyId",
                table: "FamilyWorshipRecords");

            migrationBuilder.DropIndex(
                name: "IX_FamilyWorshipRecords_FromBabyId",
                table: "FamilyWorshipRecords");

            migrationBuilder.DropIndex(
                name: "IX_FamilyWorshipRecords_ToBabyId",
                table: "FamilyWorshipRecords");

            migrationBuilder.DropColumn(
                name: "FromBabyId",
                table: "FamilyWorshipRecords");

            migrationBuilder.DropColumn(
                name: "ToBabyId",
                table: "FamilyWorshipRecords");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class upgrade_Friend_For_Set_PlayerId_And_FriendId_Unique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Friends_PlayerId_FriendId",
                table: "Friends",
                columns: new[] { "PlayerId", "FriendId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Friends_PlayerId_FriendId",
                table: "Friends");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class addquestionbank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dramas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RelationDegree = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    DramaType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dramas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DramaQuestionLibrarys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    DramaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DramaQuestionLibrarys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DramaQuestionLibrarys_Dramas_DramaId",
                        column: x => x.DramaId,
                        principalTable: "Dramas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerDramas",
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
                    PlayerId = table.Column<Guid>(nullable: false),
                    DramId = table.Column<int>(nullable: false),
                    IsSkilled = table.Column<bool>(nullable: false),
                    DramaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerDramas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerDramas_Dramas_DramaId",
                        column: x => x.DramaId,
                        principalTable: "Dramas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerDramas_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionBanks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DramaQuestionLibraryId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    Sort = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionBanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionBanks_DramaQuestionLibrarys_DramaQuestionLibraryId",
                        column: x => x.DramaQuestionLibraryId,
                        principalTable: "DramaQuestionLibrarys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionBanks_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoryLines",
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
                    PlayerAId = table.Column<Guid>(nullable: false),
                    PlayerBId = table.Column<Guid>(nullable: false),
                    DramaQuestionLibraryId = table.Column<int>(nullable: false),
                    QuestionBankId = table.Column<int>(nullable: false),
                    isStageCleared = table.Column<bool>(nullable: false),
                    DramaId = table.Column<int>(nullable: true),
                    PlayerId = table.Column<Guid>(nullable: true),
                    PlayerId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryLines_Dramas_DramaId",
                        column: x => x.DramaId,
                        principalTable: "Dramas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoryLines_DramaQuestionLibrarys_DramaQuestionLibraryId",
                        column: x => x.DramaQuestionLibraryId,
                        principalTable: "DramaQuestionLibrarys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoryLines_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoryLines_Players_PlayerId1",
                        column: x => x.PlayerId1,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DramaQuestionLibrarys_DramaId",
                table: "DramaQuestionLibrarys",
                column: "DramaId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerDramas_DramaId",
                table: "PlayerDramas",
                column: "DramaId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerDramas_PlayerId",
                table: "PlayerDramas",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBanks_DramaQuestionLibraryId",
                table: "QuestionBanks",
                column: "DramaQuestionLibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBanks_QuestionId",
                table: "QuestionBanks",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryLines_DramaId",
                table: "StoryLines",
                column: "DramaId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryLines_DramaQuestionLibraryId",
                table: "StoryLines",
                column: "DramaQuestionLibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryLines_PlayerId",
                table: "StoryLines",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryLines_PlayerId1",
                table: "StoryLines",
                column: "PlayerId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerDramas");

            migrationBuilder.DropTable(
                name: "QuestionBanks");

            migrationBuilder.DropTable(
                name: "StoryLines");

            migrationBuilder.DropTable(
                name: "DramaQuestionLibrarys");

            migrationBuilder.DropTable(
                name: "Dramas");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Add_GameRecord_UpGraded_GameTask_And_AnswerQuestion_To_BSMP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameTasks",
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
                    State = table.Column<int>(nullable: false),
                    TaskType = table.Column<int>(nullable: false),
                    AppointmentContent = table.Column<string>(maxLength: 300, nullable: true),
                    AlreadyAnsweredId = table.Column<Guid>(nullable: false),
                    FriendRelationship = table.Column<string>(maxLength: 30, nullable: true),
                    SeekType = table.Column<int>(nullable: false),
                    InviterPlayerId = table.Column<Guid>(nullable: false),
                    InviteePlayerId = table.Column<Guid>(nullable: false),
                    InvitationLink = table.Column<string>(nullable: true),
                    IsInvalid = table.Column<bool>(nullable: false),
                    Sort = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnswerQuestions",
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
                    InviterAnswer = table.Column<string>(maxLength: 300, nullable: true),
                    InviteeAnswer = table.Column<string>(maxLength: 300, nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    State = table.Column<bool>(nullable: false),
                    QuesionId = table.Column<int>(nullable: false),
                    GameTaskId = table.Column<Guid>(nullable: false),
                    Sort = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerQuestions_GameTasks_GameTaskId",
                        column: x => x.GameTaskId,
                        principalTable: "GameTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerQuestions_Questions_QuesionId",
                        column: x => x.QuesionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameRecords",
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
                    RecordTime = table.Column<DateTime>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    GameTaskId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameRecords_GameTasks_GameTaskId",
                        column: x => x.GameTaskId,
                        principalTable: "GameTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerQuestions_GameTaskId",
                table: "AnswerQuestions",
                column: "GameTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerQuestions_QuesionId",
                table: "AnswerQuestions",
                column: "QuesionId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRecords_GameTaskId",
                table: "GameRecords",
                column: "GameTaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerQuestions");

            migrationBuilder.DropTable(
                name: "GameRecords");

            migrationBuilder.DropTable(
                name: "GameTasks");
        }
    }
}

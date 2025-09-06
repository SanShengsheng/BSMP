using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class Intial_ChineseBaby_To_BSMP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BabyActivities",
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
                    Title = table.Column<string>(nullable: true),
                    Cost = table.Column<int>(nullable: false),
                    CostType = table.Column<int>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BabyEndings",
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
                    Intelligence = table.Column<int>(nullable: false),
                    Physique = table.Column<int>(nullable: false),
                    Imagine = table.Column<int>(nullable: false),
                    WillPower = table.Column<int>(nullable: false),
                    EmotionQuotient = table.Column<int>(nullable: false),
                    Charm = table.Column<int>(nullable: false),
                    Healthy = table.Column<int>(nullable: false),
                    Energy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    MaxIntelligence = table.Column<int>(nullable: false),
                    MaxPhysique = table.Column<int>(nullable: false),
                    MaxImagine = table.Column<int>(nullable: false),
                    MaxWillPower = table.Column<int>(nullable: false),
                    MaxEmotionQuotient = table.Column<int>(nullable: false),
                    MaxCharm = table.Column<int>(nullable: false),
                    MaxHealthy = table.Column<int>(nullable: false),
                    MaxEnergy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyEndings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BabyGrowUpRecords",
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
                    Intelligence = table.Column<int>(nullable: false),
                    Physique = table.Column<int>(nullable: false),
                    Imagine = table.Column<int>(nullable: false),
                    WillPower = table.Column<int>(nullable: false),
                    EmotionQuotient = table.Column<int>(nullable: false),
                    Charm = table.Column<int>(nullable: false),
                    Healthy = table.Column<int>(nullable: false),
                    Energy = table.Column<int>(nullable: false),
                    BabyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyGrowUpRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventGroups",
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
                    PrevGroupId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventGroups_EventGroups_PrevGroupId",
                        column: x => x.PrevGroupId,
                        principalTable: "EventGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Families",
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
                    FatherId = table.Column<Guid>(nullable: false),
                    MotherId = table.Column<Guid>(nullable: false),
                    Deposit = table.Column<double>(nullable: false),
                    Happiness = table.Column<double>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ChargeAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Families", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Informations",
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
                    Content = table.Column<string>(nullable: true),
                    SenderId = table.Column<Guid>(nullable: true),
                    ReceiverId = table.Column<Guid>(nullable: false),
                    FamilyId = table.Column<int>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Informations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Informations_Players_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Informations_Players_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Professions",
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
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Grade = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Salary = table.Column<double>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true),
                    RewardId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rewards",
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
                    Intelligence = table.Column<int>(nullable: false),
                    Physique = table.Column<int>(nullable: false),
                    Imagine = table.Column<int>(nullable: false),
                    WillPower = table.Column<int>(nullable: false),
                    EmotionQuotient = table.Column<int>(nullable: false),
                    Charm = table.Column<int>(nullable: false),
                    Healthy = table.Column<int>(nullable: false),
                    Energy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rewards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Babies",
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
                    Intelligence = table.Column<int>(nullable: false),
                    Physique = table.Column<int>(nullable: false),
                    Imagine = table.Column<int>(nullable: false),
                    WillPower = table.Column<int>(nullable: false),
                    EmotionQuotient = table.Column<int>(nullable: false),
                    Charm = table.Column<int>(nullable: false),
                    Healthy = table.Column<int>(nullable: false),
                    Energy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    FamilyId = table.Column<int>(nullable: false),
                    CoverImage = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: false),
                    BabyEndingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Babies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Babies_BabyEndings_BabyEndingId",
                        column: x => x.BabyEndingId,
                        principalTable: "BabyEndings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BabyActivityRecords",
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
                    ReceiverId = table.Column<Guid>(nullable: false),
                    ActivityId = table.Column<int>(nullable: false),
                    FamilyId = table.Column<int>(nullable: false),
                    IsGet = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyActivityRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyActivityRecords_BabyActivities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "BabyActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BabyActivityRecords_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BabyEventRecords",
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
                    FamilyId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true),
                    OptionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyEventRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyEventRecords_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BabyEventRecords_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChangeProfessionCosts",
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
                    ProfessionId = table.Column<int>(nullable: false),
                    CostType = table.Column<int>(nullable: false),
                    Cost = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeProfessionCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChangeProfessionCosts_Professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "Professions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerProfessions",
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
                    PlayerId = table.Column<Guid>(nullable: false),
                    FamilyId = table.Column<int>(nullable: false),
                    ProfessionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerProfessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerProfessions_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerProfessions_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerProfessions_Professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "Professions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BabyEvents",
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
                    Type = table.Column<int>(nullable: false),
                    IsBlock = table.Column<bool>(nullable: false),
                    OperationType = table.Column<int>(nullable: false),
                    Aside = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CountDown = table.Column<int>(nullable: false),
                    ConditionType = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: true),
                    BabyProperty = table.Column<int>(nullable: true),
                    MaxValue = table.Column<int>(nullable: true),
                    MinValue = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    ActivityId = table.Column<int>(nullable: true),
                    RewardId = table.Column<int>(nullable: true),
                    ConsumeId = table.Column<int>(nullable: true),
                    EventGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyEvents_Rewards_ConsumeId",
                        column: x => x.ConsumeId,
                        principalTable: "Rewards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BabyEvents_EventGroups_EventGroupId",
                        column: x => x.EventGroupId,
                        principalTable: "EventGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BabyEvents_Rewards_RewardId",
                        column: x => x.RewardId,
                        principalTable: "Rewards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BabyEventOptions",
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
                    Content = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    BabyEventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyEventOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyEventOptions_BabyEvents_BabyEventId",
                        column: x => x.BabyEventId,
                        principalTable: "BabyEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventGroupBabyEvents",
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
                    GroupId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    EventGroupId = table.Column<int>(nullable: true),
                    BabyEventId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventGroupBabyEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventGroupBabyEvents_BabyEvents_BabyEventId",
                        column: x => x.BabyEventId,
                        principalTable: "BabyEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventGroupBabyEvents_EventGroups_EventGroupId",
                        column: x => x.EventGroupId,
                        principalTable: "EventGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Babies_BabyEndingId",
                table: "Babies",
                column: "BabyEndingId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyActivityRecords_ActivityId",
                table: "BabyActivityRecords",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyActivityRecords_FamilyId",
                table: "BabyActivityRecords",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyEventOptions_BabyEventId",
                table: "BabyEventOptions",
                column: "BabyEventId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyEventRecords_FamilyId",
                table: "BabyEventRecords",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyEventRecords_PlayerId",
                table: "BabyEventRecords",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyEvents_ConsumeId",
                table: "BabyEvents",
                column: "ConsumeId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyEvents_EventGroupId",
                table: "BabyEvents",
                column: "EventGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyEvents_RewardId",
                table: "BabyEvents",
                column: "RewardId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeProfessionCosts_ProfessionId",
                table: "ChangeProfessionCosts",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_EventGroupBabyEvents_BabyEventId",
                table: "EventGroupBabyEvents",
                column: "BabyEventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventGroupBabyEvents_EventGroupId",
                table: "EventGroupBabyEvents",
                column: "EventGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EventGroups_PrevGroupId",
                table: "EventGroups",
                column: "PrevGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Informations_ReceiverId",
                table: "Informations",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Informations_SenderId",
                table: "Informations",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerProfessions_FamilyId",
                table: "PlayerProfessions",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerProfessions_PlayerId",
                table: "PlayerProfessions",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerProfessions_ProfessionId",
                table: "PlayerProfessions",
                column: "ProfessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Babies");

            migrationBuilder.DropTable(
                name: "BabyActivityRecords");

            migrationBuilder.DropTable(
                name: "BabyEventOptions");

            migrationBuilder.DropTable(
                name: "BabyEventRecords");

            migrationBuilder.DropTable(
                name: "BabyGrowUpRecords");

            migrationBuilder.DropTable(
                name: "ChangeProfessionCosts");

            migrationBuilder.DropTable(
                name: "EventGroupBabyEvents");

            migrationBuilder.DropTable(
                name: "Informations");

            migrationBuilder.DropTable(
                name: "PlayerProfessions");

            migrationBuilder.DropTable(
                name: "BabyEndings");

            migrationBuilder.DropTable(
                name: "BabyActivities");

            migrationBuilder.DropTable(
                name: "BabyEvents");

            migrationBuilder.DropTable(
                name: "Families");

            migrationBuilder.DropTable(
                name: "Professions");

            migrationBuilder.DropTable(
                name: "Rewards");

            migrationBuilder.DropTable(
                name: "EventGroups");
        }
    }
}

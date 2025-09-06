using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MQKJ.BSMP.Migrations
{
    public partial class propStoreini : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "CoolingInterval",
            //    table: "Families",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "FightCount",
            //    table: "Families",
            //    maxLength: 20,
            //    nullable: false,
            //    defaultValue: 10);

            migrationBuilder.CreateTable(
                name: "BabyAssetFeatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FamilyId = table.Column<int>(nullable: false),
                    BabyId = table.Column<int>(nullable: false),
                    AssetFeatureProperty = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyAssetFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BabyPropFeatureTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Code = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    EventAdditionType = table.Column<int>(nullable: false),
                    Group = table.Column<int>(nullable: false),
                    IsAddition = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyPropFeatureTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BabyPropPropertyAwards",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Charm = table.Column<int>(nullable: false),
                    EmotionQuotient = table.Column<int>(nullable: false),
                    Imagine = table.Column<int>(nullable: false),
                    Intelligence = table.Column<int>(nullable: false),
                    Physique = table.Column<int>(nullable: false),
                    WillPower = table.Column<int>(nullable: false),
                    EventAdditionType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyPropPropertyAwards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BabyPropTypes",
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
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Img = table.Column<string>(nullable: true),
                    Code = table.Column<int>(nullable: false),
                    Sort = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    EquipmentAbleObject = table.Column<int>(nullable: false),
                    MaxEquipmentCount = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyPropTypes", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "BuyFightCountRecords",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false),
            //        CreationTime = table.Column<DateTime>(nullable: false),
            //        CreatorUserId = table.Column<long>(nullable: true),
            //        LastModificationTime = table.Column<DateTime>(nullable: true),
            //        LastModifierUserId = table.Column<long>(nullable: true),
            //        DeleterUserId = table.Column<long>(nullable: true),
            //        DeletionTime = table.Column<DateTime>(nullable: true),
            //        IsDeleted = table.Column<bool>(nullable: false),
            //        PurchaserId = table.Column<Guid>(nullable: true),
            //        FamilyId = table.Column<int>(nullable: true),
            //        FightCount = table.Column<int>(nullable: false),
            //        CoinCount = table.Column<double>(nullable: false),
            //        Description = table.Column<string>(nullable: true),
            //        SourceType = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_BuyFightCountRecords", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_BuyFightCountRecords_Families_FamilyId",
            //            column: x => x.FamilyId,
            //            principalTable: "Families",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_BuyFightCountRecords_Players_PurchaserId",
            //            column: x => x.PurchaserId,
            //            principalTable: "Players",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            migrationBuilder.CreateTable(
                name: "FamilyCoinDepositChangeRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    StakeholderId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    BabyId = table.Column<int>(nullable: true),
                    FamilyId = table.Column<int>(nullable: false),
                    CostType = table.Column<int>(nullable: false),
                    GetWay = table.Column<int>(nullable: false),
                    CurrentFamilyCoinDeposit = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyCoinDepositChangeRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FamilyCoinDepositChangeRecords_Babies_BabyId",
                        column: x => x.BabyId,
                        principalTable: "Babies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FamilyCoinDepositChangeRecords_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FamilyCoinDepositChangeRecords_Players_StakeholderId",
                        column: x => x.StakeholderId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BabyPropBuyTermTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Code = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    BabyPropTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyPropBuyTermTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyPropBuyTermTypes_BabyPropTypes_BabyPropTypeId",
                        column: x => x.BabyPropTypeId,
                        principalTable: "BabyPropTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BabyProps",
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
                    Title = table.Column<string>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    BabyPropPropertyAwardId = table.Column<int>(nullable: true),
                    BabyPropPropertyAwardId1 = table.Column<Guid>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    DiscountInfo = table.Column<string>(nullable: true),
                    IsNewProp = table.Column<bool>(nullable: false),
                    MaxPurchasesNumber = table.Column<int>(nullable: false),
                    CoverImg = table.Column<string>(nullable: true),
                    BabyPropTypeId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsAfterBuyPlayMarquees = table.Column<bool>(nullable: false),
                    PropValue = table.Column<decimal>(nullable: false),
                    GetWay = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: true),
                    Code = table.Column<int>(nullable: false),
                    IsInheritAble = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyProps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyProps_BabyPropPropertyAwards_BabyPropPropertyAwardId1",
                        column: x => x.BabyPropPropertyAwardId1,
                        principalTable: "BabyPropPropertyAwards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BabyProps_BabyPropTypes_BabyPropTypeId",
                        column: x => x.BabyPropTypeId,
                        principalTable: "BabyPropTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BabyAssetFeatureRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BabyPropId = table.Column<int>(nullable: false),
                    FamilyId = table.Column<int>(nullable: false),
                    BabyId = table.Column<int>(nullable: false),
                    AssetFeatureProperty = table.Column<string>(nullable: true),
                    LastAssetFeatureProperty = table.Column<string>(nullable: true),
                    BabyAssetFeatureId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyAssetFeatureRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyAssetFeatureRecords_BabyAssetFeatures_BabyAssetFeatureId",
                        column: x => x.BabyAssetFeatureId,
                        principalTable: "BabyAssetFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BabyAssetFeatureRecords_BabyProps_BabyPropId",
                        column: x => x.BabyPropId,
                        principalTable: "BabyProps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BabyFamilyAssets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FamilyId = table.Column<int>(nullable: false),
                    OwnId = table.Column<int>(nullable: true),
                    BabyPropId = table.Column<int>(nullable: false),
                    ExpiredDateTime = table.Column<DateTime>(nullable: true),
                    IsEquipmenting = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyFamilyAssets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyFamilyAssets_BabyProps_BabyPropId",
                        column: x => x.BabyPropId,
                        principalTable: "BabyProps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BabyFamilyAssets_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BabyFamilyAssets_Babies_OwnId",
                        column: x => x.OwnId,
                        principalTable: "Babies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BabyPropFeatures",
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
                    BabyPropId = table.Column<int>(nullable: false),
                    BabyPropFeatureTypeId = table.Column<int>(nullable: false),
                    BabyPropFeatureTypeId1 = table.Column<Guid>(nullable: true),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyPropFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyPropFeatures_BabyPropFeatureTypes_BabyPropFeatureTypeId1",
                        column: x => x.BabyPropFeatureTypeId1,
                        principalTable: "BabyPropFeatureTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BabyPropFeatures_BabyProps_BabyPropId",
                        column: x => x.BabyPropId,
                        principalTable: "BabyProps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BabyPropPrices",
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
                    CurrencyType = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Icon = table.Column<string>(nullable: true),
                    BabyPropId = table.Column<int>(nullable: false),
                    Dsecription = table.Column<string>(nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    Validity = table.Column<double>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyPropPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyPropPrices_BabyProps_BabyPropId",
                        column: x => x.BabyPropId,
                        principalTable: "BabyProps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BabyPropRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FamilyId = table.Column<int>(nullable: false),
                    PurchaserId = table.Column<Guid>(nullable: false),
                    BabyPropId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: true),
                    PropSource = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyPropRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyPropRecords_BabyProps_BabyPropId",
                        column: x => x.BabyPropId,
                        principalTable: "BabyProps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BabyPropRecords_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BabyPropRecords_Players_PurchaserId",
                        column: x => x.PurchaserId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BabyPropTerms",
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
                    BabyPropId = table.Column<int>(nullable: false),
                    BabyPropBuyTermId = table.Column<Guid>(nullable: false),
                    MaxValue = table.Column<int>(nullable: true),
                    MinValue = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyPropTerms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyPropTerms_BabyPropBuyTermTypes_BabyPropBuyTermId",
                        column: x => x.BabyPropBuyTermId,
                        principalTable: "BabyPropBuyTermTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BabyPropTerms_BabyProps_BabyPropId",
                        column: x => x.BabyPropId,
                        principalTable: "BabyProps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BabyAssetAwards",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BabyFamilyAssetId = table.Column<int>(nullable: false),
                    BabyFamilyAssetId1 = table.Column<Guid>(nullable: true),
                    FamilyId = table.Column<int>(nullable: false),
                    ExpiredDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyAssetAwards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyAssetAwards_BabyFamilyAssets_BabyFamilyAssetId1",
                        column: x => x.BabyFamilyAssetId1,
                        principalTable: "BabyFamilyAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BabyAssetRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FamilyAssetId = table.Column<Guid>(nullable: false),
                    FamilyId = table.Column<int>(nullable: false),
                    BabyId = table.Column<int>(nullable: false),
                    EquipmentState = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyAssetRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyAssetRecords_Babies_BabyId",
                        column: x => x.BabyId,
                        principalTable: "Babies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BabyAssetRecords_BabyFamilyAssets_FamilyAssetId",
                        column: x => x.FamilyAssetId,
                        principalTable: "BabyFamilyAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BabyAssetRecords_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BabyAssetAwards_BabyFamilyAssetId1",
                table: "BabyAssetAwards",
                column: "BabyFamilyAssetId1");

            migrationBuilder.CreateIndex(
                name: "IX_BabyAssetFeatureRecords_BabyAssetFeatureId",
                table: "BabyAssetFeatureRecords",
                column: "BabyAssetFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyAssetFeatureRecords_BabyPropId",
                table: "BabyAssetFeatureRecords",
                column: "BabyPropId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyAssetRecords_BabyId",
                table: "BabyAssetRecords",
                column: "BabyId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyAssetRecords_FamilyAssetId",
                table: "BabyAssetRecords",
                column: "FamilyAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyAssetRecords_FamilyId",
                table: "BabyAssetRecords",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyFamilyAssets_BabyPropId",
                table: "BabyFamilyAssets",
                column: "BabyPropId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyFamilyAssets_FamilyId",
                table: "BabyFamilyAssets",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyFamilyAssets_OwnId",
                table: "BabyFamilyAssets",
                column: "OwnId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyPropBuyTermTypes_BabyPropTypeId",
                table: "BabyPropBuyTermTypes",
                column: "BabyPropTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyPropFeatures_BabyPropFeatureTypeId1",
                table: "BabyPropFeatures",
                column: "BabyPropFeatureTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_BabyPropFeatures_BabyPropId",
                table: "BabyPropFeatures",
                column: "BabyPropId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyPropPrices_BabyPropId",
                table: "BabyPropPrices",
                column: "BabyPropId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyPropRecords_BabyPropId",
                table: "BabyPropRecords",
                column: "BabyPropId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyPropRecords_FamilyId",
                table: "BabyPropRecords",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyPropRecords_PurchaserId",
                table: "BabyPropRecords",
                column: "PurchaserId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyProps_BabyPropPropertyAwardId1",
                table: "BabyProps",
                column: "BabyPropPropertyAwardId1");

            migrationBuilder.CreateIndex(
                name: "IX_BabyProps_BabyPropTypeId",
                table: "BabyProps",
                column: "BabyPropTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyPropTerms_BabyPropBuyTermId",
                table: "BabyPropTerms",
                column: "BabyPropBuyTermId");

            migrationBuilder.CreateIndex(
                name: "IX_BabyPropTerms_BabyPropId",
                table: "BabyPropTerms",
                column: "BabyPropId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_BuyFightCountRecords_FamilyId",
            //    table: "BuyFightCountRecords",
            //    column: "FamilyId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_BuyFightCountRecords_PurchaserId",
            //    table: "BuyFightCountRecords",
            //    column: "PurchaserId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyCoinDepositChangeRecords_BabyId",
                table: "FamilyCoinDepositChangeRecords",
                column: "BabyId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyCoinDepositChangeRecords_FamilyId",
                table: "FamilyCoinDepositChangeRecords",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyCoinDepositChangeRecords_StakeholderId",
                table: "FamilyCoinDepositChangeRecords",
                column: "StakeholderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BabyAssetAwards");

            migrationBuilder.DropTable(
                name: "BabyAssetFeatureRecords");

            migrationBuilder.DropTable(
                name: "BabyAssetRecords");

            migrationBuilder.DropTable(
                name: "BabyPropFeatures");

            migrationBuilder.DropTable(
                name: "BabyPropPrices");

            migrationBuilder.DropTable(
                name: "BabyPropRecords");

            migrationBuilder.DropTable(
                name: "BabyPropTerms");

            //migrationBuilder.DropTable(
            //    name: "BuyFightCountRecords");

            migrationBuilder.DropTable(
                name: "FamilyCoinDepositChangeRecords");

            migrationBuilder.DropTable(
                name: "BabyAssetFeatures");

            migrationBuilder.DropTable(
                name: "BabyFamilyAssets");

            migrationBuilder.DropTable(
                name: "BabyPropFeatureTypes");

            migrationBuilder.DropTable(
                name: "BabyPropBuyTermTypes");

            migrationBuilder.DropTable(
                name: "BabyProps");

            migrationBuilder.DropTable(
                name: "BabyPropPropertyAwards");

            migrationBuilder.DropTable(
                name: "BabyPropTypes");

            //migrationBuilder.DropColumn(
            //    name: "CoolingInterval",
            //    table: "Families");

            //migrationBuilder.DropColumn(
            //    name: "FightCount",
            //    table: "Families");
        }
    }
}

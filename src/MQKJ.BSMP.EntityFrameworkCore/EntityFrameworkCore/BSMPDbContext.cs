using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.ActiveApply;
using MQKJ.BSMP.Answers;
using MQKJ.BSMP.ApplicationLogs;
using MQKJ.BSMP.Authorization.Roles;
using MQKJ.BSMP.Authorization.Users;
using MQKJ.BSMP.BonusPoints;
using MQKJ.BSMP.BSMPFiles;
using MQKJ.BSMP.DramaQuestionLibraryTypes;
using MQKJ.BSMP.Dramas;
using MQKJ.BSMP.Emoticons;
using MQKJ.BSMP.Feedbacks;
using MQKJ.BSMP.Friends;
using MQKJ.BSMP.GameRecords;
using MQKJ.BSMP.GameTasks;
using MQKJ.BSMP.Likes;
using MQKJ.BSMP.LoveCardFiles;
using MQKJ.BSMP.MultiTenancy;
using MQKJ.BSMP.PlayerDramas;
using MQKJ.BSMP.Players;
using MQKJ.BSMP.PropUseRecords;
using MQKJ.BSMP.QuestionBankRules;
using MQKJ.BSMP.QuestionBanks;
using MQKJ.BSMP.Questions;
using MQKJ.BSMP.SceneFiles;
using MQKJ.BSMP.Scenes;
using MQKJ.BSMP.SceneTypes;
using MQKJ.BSMP.StaminaRecords;
using MQKJ.BSMP.StoryLines;
using MQKJ.BSMP.Tags;
using MQKJ.BSMP.TagTypes;
using MQKJ.BSMP.TextAudios;
using System;
using System.Linq;
using System.Reflection;
using MQKJ.BSMP.LoveCards;
using MQKJ.BSMP.PlayerLabels;
using MQKJ.BSMP.LoveCardOptions;
using MQKJ.BSMP.Orders;
using MQKJ.BSMP.UnLocks;
using MQKJ.BSMP.Products;
using MQKJ.BSMP.ChineseBabies;
using MQKJ.BSMP.Common.SensitiveWords;
using MQKJ.BSMP.Common;
using MQKJ.BSMP.Common.MqAgents;
using MQKJ.BSMP.Common.IncomeRecords;
using MQKJ.BSMP.Common.WechatPay;
using MQKJ.BSMP.SystemMessages;
using MQKJ.BSMP.Common.OperationActivities;
using MQKJ.BSMP.ChineseBabies.PropMall;
using MQKJ.BSMP.ChineseBabies.Backpack;
using MQKJ.BSMP.ChineseBabies.Athletics;
using MQKJ.BSMP.Common.RunHorseInformations;
using MQKJ.BSMP.ChineseBabies.Asset;
using MQKJ.BSMP.ChineseBabies.Prestiges;
using MQKJ.BSMP.Common.Companies;
using System.Data.SqlClient;
using MQKJ.BSMP.Common.AD;

namespace MQKJ.BSMP.EntityFrameworkCore
{
    public class BSMPDbContext : AbpZeroDbContext<Tenant, Role, User, BSMPDbContext>
    {
        /* Define a DbSet for each entity of the application */
        #region 恋习大冒险
        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<TagType> TagType { get; set; }

        public DbSet<QuestionTag> QuestionTags { get; set; }

        public DbSet<Scene> Scenes { get; set; }

        public DbSet<SceneFile> SceneImgs { get; set; }

        public DbSet<SceneType> SceneTypes { get; set; }
        public DbSet<GameTask> GameTasks { get; set; }

        public DbSet<AnswerQuestion> AnswerQuestions { get; set; }

        public DbSet<GameRecord> GameRecords { get; set; }

        public DbSet<BonusPoint> BonusPoints { get; set; }

        public DbSet<BonusPointRecord> BonusPointRecords { get; set; }

        public DbSet<PropUseRecord> PropUseRecords { get; set; }

        public DbSet<EmoticonRecord> EmoticonRecords { get; set; }


        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Friend> Friends { get; set; }

        public DbSet<StaminaRecord> StaminaRecords { get; set; }

        public DbSet<TextAudio> TextAudios { get; set; }

        public DbSet<RiskActiveApply> RiskActiveApplys { get; set; }



        #region 题库
        public DbSet<Drama> Dramas { get; set; }
        public DbSet<DramaQuestionLibrary> DramaQuestionLibrarys { get; set; }
        public DbSet<QuestionBank> QuestionBanks { get; set; }
        public DbSet<StoryLine> StoryLines { get; set; }
        public DbSet<PlayerDrama> PlayerDramas { get; set; }
        public DbSet<QuestionBankRule> QuestionBankRules { get; set; }
        #endregion
        #endregion

        #region 恋爱名片
   public DbSet<LoveCardFile> LoveCardFiles { get; set; }
        public DbSet<Like> Likes { get; set; }

        public DbSet<LoveCardOption> LoveCardOptions { get; set; }

        public DbSet<LoveCard> LoveCards { get; set; }



        #endregion
        public DbSet<ApplicationLog> ApplicationLogs { get; set; }

     
        public DbSet<BSMPFile> BSMPFiles { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<PlayerExtension> PlayerExtensions { get; set; }

        public DbSet<PlayerLabel> PlayerLabels { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Unlock> Unlocks { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<WechatPayNotify> WechatPayNotifies { get; set; }
        public DbSet<WechatMpUser> MpUsers { get; set; }

        public DbSet<SystemMessage> SystemMessages { get; set; }

        public DbSet<RunHorseInformation> RunHorseInformations { get; set; }

        public DbSet<WechatMerchant> WechatMerchants { get; set; }
        public DbSet<Adviertisement> Adviertisements { get; set; }
        public DbSet<PlayerAdviertisement> PlayerAdviertisements { get; set; }


        #region 养娃
        public DbSet<Baby> Babies { get; set; }

        public DbSet<Family> Families { get; set; }

        public DbSet<DismissFamilyRecord> DismissFamilyRecords { get; set; }

        public DbSet<BabyActivity> BabyActivities { get; set; }

        public DbSet<BabyActivityRecord> BabyActivityRecords { get; set; }

        public DbSet<BabyEnding> BabyEndings { get; set; }
        public DbSet<BabyEventOption> BabyEventOptions { get; set; }

        public DbSet<BabyEvent> BabyEvents { get; set; }

        public DbSet<BabyEventRecord> BabyEventRecords { get; set; }

        public DbSet<BabyGrowUpRecord> BabyGrowUpRecords { get; set; }

        public DbSet<ChangeProfessionCost> ChangeProfessionCosts { get; set; }

        public DbSet<EventGroup> EventGroups { get; set; }

        public DbSet<EventGroupBabyEvent> EventGroupBabyEvents { get; set; }

        public DbSet<Information> Informations { get; set; }

        public DbSet<Profession> Professions { get; set; }

        public DbSet<PlayerProfession> PlayerProfessions { get; set; }

        public DbSet<MqAgent> MqAgents { get; set; }

        public DbSet<Reward> Rewards { get; set; }

        /// <summary>
        /// 敏感词
        /// </summary>
        public DbSet<SensitiveWord> SensitiveWords { get; set; }

        public DbSet<CoinRecharge> CoinRecharges { get; set; }


        public DbSet<EnergyRecharge> EnergyRecharges { get; set; }

        public DbSet<CoinRechargeRecord> CoinRechargeRecords { get; set; }

        public DbSet<EnergyRechargeRecord> EnergyRechargeRecords { get; set; }
        public DbSet<AutoRunnerConfig> AutoRunnerConfigs { get; set; }
        public DbSet<AutoRunnerRecord> AutoRunnerRecords { get; set; }

        public DbSet<EnterpirsePaymentRecord> EnterpirsePaymentRecords { get; set; }

        public DbSet<AgentInviteCode> AgentInviteCodes { get; set; }

        public DbSet<IncomeRecord> IncomeRecords { get; set; }
        /// <summary>
        /// 系统设置
        /// </summary>
        public DbSet<SystemSetting> SystemSettings { get; set; }

        public DbSet<SupplementCoinRecord> SupplementCoinRecords { get; set; }

        public DbSet<ImportDataRecord> ImportDataRecords { get; set; }

        public DbSet<WeChatWebUser> WeChatWebUsers { get; set; }
        /// <summary>
        /// 版本管理
        /// </summary>
        public DbSet<VersionManage> VersionManages { get; set; }
        /// <summary>
        /// 运营活动
        /// </summary>
        public DbSet<OperationActivity> OperationActivities { get; set; }


        public DbSet<BuyFightCountRecord> BuyFightCountRecords { get; set; }

        public DbSet<Competition> Competitions { get; set; }

        public DbSet<FightRecord> FightRecords { get; set; }


        public DbSet<SeasonManagement> SeasonManagements { get; set; }


        public DbSet<AthleticsReward> AthleticsRewards { get; set; }

        public DbSet<AthleticsRewardRecord> AthleticsRewardRecords { get; set; }

        public DbSet<AthleticsInformation> AthleticsInformations { get; set; }


        /// <summary>
        /// 家庭金币资产变更记录
        /// </summary>
        public DbSet<FamilyCoinDepositChangeRecord> FamilyCoinDepositChangeRecords { get; set; }
        #region 商城&家庭资产
        /// <summary>
        /// 道具
        /// </summary>
        public DbSet<BabyProp> BabyProps { get; set; }
        /// <summary>
        /// 道具条件类型
        /// </summary>
        public DbSet<BabyPropBuyTermType> BabyPropBuyTermTypes { get; set; }
        /// <summary>
        /// 道具特性
        /// </summary>
        public DbSet<BabyPropFeature> BabyPropFeatures { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public DbSet<BabyPropPrice> BabyPropPrices { get; set; }
        /// <summary>
        /// 商品功能(属性)
        /// </summary>
        public DbSet<BabyPropFeatureType> BabyPropFeatureTypes { get; set; }
        /// <summary>
        /// 获得道具记录
        /// </summary>
        public DbSet<BabyPropRecord> BabyPropRecords { get; set; }
        /// <summary>
        /// 宝宝道具条件
        /// </summary>
        public DbSet<BabyPropTerm> BabyPropTerms { get; set; }
        /// <summary>
        /// 道具类别
        /// </summary>
        public DbSet<BabyPropType> BabyPropTypes { get; set; }
        /// <summary>
        /// 装备加成
        /// </summary>
        public DbSet<BabyAssetFeature> BabyAssetFeatures { get; set; }
        /// <summary>
        /// 宝宝装备加成记录
        /// </summary>
        public DbSet<BabyAssetFeatureRecord> BabyAssetFeatureRecords { get; set; }
       
        /// <summary>
        /// 宝宝装备加成记录
        /// </summary>
        public DbSet<BabyFamilyAsset> BabyFamilyAssets { get; set; }

        /// <summary>
        /// 装备记录，记录装备的使用，卸下的操作历史
        /// </summary>
        public DbSet<BabyAssetRecord> BabyAssetRecords { get; set; }
        /// <summary>
        /// 道具属性奖励
        /// </summary>
        public DbSet<BabyPropPropertyAward> BabyPropPropertyAwards { get; set; }
        /// <summary>
        /// 装备属性加成
        /// </summary>
        public DbSet<BabyAssetAward> BabyAssetAwards { get; set; }
        /// <summary>
        /// 道具礼包（套餐）
        /// </summary>
        public DbSet<BabyPropBag> BabyPropBags { get; set; }
        /// <summary>
        /// 道具大礼包&道具中间表
        /// </summary>
        public DbSet<BabyPropBagAndBabyProp> BabyPropBagAndBabyProps { get; set; }
        #endregion

        #region 家庭膜拜feature
        /// <summary>
        /// 家庭膜拜记录表
        /// </summary>
        public DbSet<FamilyWorshipRecord> FamilyWorshipRecords { get; set; }
        /// <summary>
        /// 家庭膜拜奖励表
        /// </summary>

        public DbSet<FamilyWorshipReward> FamilyWorshipRewards { get; set; }
        #endregion

        #endregion

        public DbSet<Company> Companies  { get; set; }

        public BSMPDbContext(DbContextOptions<BSMPDbContext> options)
            : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Scene>(s =>
            //{
            //    s.HasIndex(e => new { e.SceneName }).IsUnique();
            //});

            //modelBuilder.Entity<Friend>(f =>
            //{
            //    f.HasIndex(p => new { p.PlayerId,p.FriendId }).IsUnique();
            //});

            //查找所有FluentAPI配置
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes().Where(q => q.GetInterface(typeof(IEntityTypeConfiguration<>).FullName) != null);

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasMany(q => q.Answers)
                    .WithOne(a => a.Question)
                    .HasForeignKey(a => a.QuestionID);
                

                entity.HasOne(a => a.CheckOne)
                    .WithMany()
                    .HasForeignKey(a => a.CheckOneId);

                entity.HasOne(a => a.DefaultImg)
                    .WithMany(a => a.Questions)
                    .HasForeignKey(a => a.DefaultImgId)
                    .HasConstraintName("FK_Questions_BSMPFiles_DefaultImgId");
            });
            
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasOne(p => p.PlayerExtension)
                    .WithOne(p => p.Player)
                    .HasForeignKey<PlayerExtension>(p => p.PlayerGuid)
                    .IsRequired(true);
            });
            //应用FluentAPI
            foreach (var type in typesToRegister)
            {
                //dynamic使C#具有弱语言的特性，在编译时不对类型进行检查

                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        var conn = new SqlConnectionStringBuilder("Data Source=.;Initial Catalog=BSMP;Integrated Security=True;MultipleActiveResultSets=true")
        //        {
        //            ConnectRetryCount = 5,
        //            Pooling = false
        //        };
        //        optionsBuilder.UseSqlServer(conn.ToString(),
        //            options => options.EnableRetryOnFailure());
        //    }
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}

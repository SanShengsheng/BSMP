using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Uow;
using Abp.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using MQKJ.BSMP.EntityFrameworkCore.Seed.BonusPoints;
using MQKJ.BSMP.EntityFrameworkCore.Seed.ChineseBaby.Athletics;
using MQKJ.BSMP.EntityFrameworkCore.Seed.ChineseBaby.Athletics.AthleticsRewards;
using MQKJ.BSMP.EntityFrameworkCore.Seed.ChineseBaby.FamilyWorships;
using MQKJ.BSMP.EntityFrameworkCore;
using MQKJ.BSMP.EntityFrameworkCore.Seed.Host;
using MQKJ.BSMP.EntityFrameworkCore.Seed.QuestionBank;
using MQKJ.BSMP.EntityFrameworkCore.Seed.Scenes;
using MQKJ.BSMP.EntityFrameworkCore.Seed.SceneTypes;
using MQKJ.BSMP.EntityFrameworkCore.Seed.Tag;
using MQKJ.BSMP.EntityFrameworkCore.Seed.TagType;
using MQKJ.BSMP.EntityFrameworkCore.Seed.Tenants;
using MQKJ.BSMP.EntityFrameworkCore.Seed.TextAudio;
using System;
using System.Transactions;
using MQKJ.BSMP.EntityFrameworkCore.Seed.Common.WechatMerchants;

namespace MQKJ.BSMP.EntityFrameworkCore.Seed
{
    public static class SeedHelper
    {
        public static void SeedHostDb(IIocResolver iocResolver)
        {
            WithDbContext<BSMPDbContext>(iocResolver, SeedHostDb);
        }

        public static void SeedHostDb(BSMPDbContext context)
        {
            context.SuppressAutoSetTenantId = true;

            // Host seed
            new InitialHostDbBuilder(context).Create();

            // Default tenant seed (in host database).
            new DefaultTenantBuilder(context).Create();
            new TenantRoleAndUserBuilder(context, 1).Create();
            new SceneTypeBuilder(context).Create();

            new TagTypeBuilder(context).Create();
            new TagBuilder(context).Create();

            new BonusPointBuilder(context).Create();

            new SceneBuilder(context).Create();

            new QuestionBankBuilder(context).Create();

            new TextAudioBuilder(context).Create();

            new Product.ProductBuilder(context).Create();

            new WechatMerchantBuilder(context).Create();

            //custom code
            #region 养娃
            new RewardBuilder(context).Create();
            new ProfessionBuilder(context).Create();
            new SensitiveWordBuilder(context).Create();
            //膜拜
            new WorshipRewardsBuilder(context).Create();
            //测试，用后删除
            new EventGroupCreator(context).Create();
            new BabyEventBuilder(context).Create();
            new EventGroupBabyEventBuilder(context).Create();
            new OptionsBuilder(context).Create();
            new BabyEndingBuilder(context).Create();
            new SystemSettingBuilder(context).Create();
            new SystemMessageBuilder(context).Create();
            // 道具商城
            new BabyPropTypeBuilder(context).Create();
            new BabyPropTermTypeBuilder(context).Create();
            new BabyPropFeatureTypeBuilder(context).Create();
            //new BabyPropPropertyAwardCreator(context).Create();
            //new BabyPropBuilder(context).Create();
            //new BabyPropFeatureBuilder(context).Create();
            //new BabyPropTermBuilder(context).Create();
            //new BabyPropPriceBuilder(context).Create();
            //new BabyPropBagBuilder(context).Create();
            new SeasonManagementBuilder(context).Create();
            //new AthleticsRewardBuilder(context).Create();
            //new BabyPropBagAndBabyPropBuilder(context).Create();
            #endregion
        }

        private static void WithDbContext<TDbContext>(IIocResolver iocResolver, Action<TDbContext> contextAction)
            where TDbContext : DbContext
        {
            using (var uowManager = iocResolver.ResolveAsDisposable<IUnitOfWorkManager>())
            {
                using (var uow = uowManager.Object.Begin(TransactionScopeOption.Suppress))
                {
                    var context = uowManager.Object.Current.GetDbContext<TDbContext>(MultiTenancySides.Host);

                    contextAction(context);

                    uow.Complete();
                }
            }
        }
    }
}

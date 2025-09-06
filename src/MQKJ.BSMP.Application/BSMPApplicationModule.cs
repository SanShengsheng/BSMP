using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MQKJ.BSMP.Authorization;
using MQKJ.BSMP.BonusPointRecords.Authorization;
using MQKJ.BSMP.BonusPointRecords.Dtos;
using MQKJ.BSMP.BonusPoints.Authorization;
using MQKJ.BSMP.BonusPoints.Dtos;
using MQKJ.BSMP.Common;
using MQKJ.BSMP.Questions.Authorization;
using MQKJ.BSMP.TagTypes.Authorization;
using MQKJ.BSMP.Players.Dtos;
using MQKJ.BSMP.GameTasks.Dtos;
using MQKJ.BSMP.GameTasks.Authorization;
using MQKJ.BSMP.Players.Authorization;
using MQKJ.BSMP.Scenes.Authorization;
using MQKJ.BSMP.SceneFiles.Authorization;
using MQKJ.BSMP.WeChat.Dtos.CustomMapper;
using MQKJ.BSMP.Likes.Authorization;
using MQKJ.BSMP.Feedbacks.Authorization;
using MQKJ.BSMP.Friends.Authorization;
using MQKJ.BSMP.Friends.Mapper;
using MQKJ.BSMP.StaminaRecords.Authorization;
using MQKJ.BSMP.Resolves;
using MQKJ.BSMP.TextAudios.Mapper;
using MQKJ.BSMP.LoveCardFiles.Mapper;
using MQKJ.BSMP.LoveCardFiles.Authorization;
using MQKJ.BSMP.LoveCardOptions.Mapper;
using MQKJ.BSMP.LoveCardOptions.Authorization;
using MQKJ.BSMP.LoveCards.Authorization;
using MQKJ.BSMP.LoveCards.Mapper;
using MQKJ.BSMP.PlayerLabels.Authorization;
using MQKJ.BSMP.PlayerLabels.Mapper;
using MQKJ.BSMP.Orders.Mapper;
using MQKJ.BSMP.Orders.Authorization;
using MQKJ.BSMP.UnLocks.Mapper;
using MQKJ.BSMP.UnLocks.Authorization;
using MQKJ.BSMP.SceneManage.SceneFiles.Dto.CustomMapper;
using MQKJ.BSMP.ChineseBabies.Mapper;
using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using MQKJ.BSMP.Mapper;
using MQKJ.BSMP.Common.Authorization;
using MQKJ.BSMP.ChineseBabies.Authorization;
using MQKJ.BSMP.Common.MqAgents.Agents;
using MQKJ.BSMP.ChineseBabies.Athetics.Competitions.Mapper;

namespace MQKJ.BSMP
{
    [DependsOn(
        typeof(BSMPCoreModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpHangfireAspNetCoreModule))
        ]
    public class BSMPApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {

            //权限
            Configuration.Authorization.Providers.Add<BSMPAuthorizationProvider>();
            //Configuration.Authorization.Providers.Add<QuestionAppAuthorizationProvider>();
            //Configuration.Authorization.Providers.Add<TagTypeAppAuthorizationProvider>();
            //Configuration.Authorization.Providers.Add<BonusPointAppAuthorizationProvider>();
            //Configuration.Authorization.Providers.Add<BonusPointRecordAppAuthorizationProvider>();
            //Configuration.Authorization.Providers.Add<GameTaskAppAuthorizationProvider>();
            //Configuration.Authorization.Providers.Add<PlayerAppAuthorizationProvider>();
            //Configuration.Authorization.Providers.Add<SceneAppAuthorizationProvider>();
            //Configuration.Authorization.Providers.Add<SceneFileAppAuthorizationProvider>();
            //Configuration.Authorization.Providers.Add<LikeAuthorizationProvider>();
            //Configuration.Authorization.Providers.Add<FeedbackAuthorizationProvider>();
            //Configuration.Authorization.Providers.Add<FriendAuthorizationProvider>();
            //Configuration.Authorization.Providers.Add<StaminaRecordAuthorizationProvider>();
            //Configuration.Authorization.Providers.Add<LoveCardFileAuthorizationProvider>();
            //Configuration.Authorization.Providers.Add<UnlockAuthorizationProvider>();
            //Configuration.Authorization.Providers.Add<LoveCardOptionAuthorizationProvider>();
            //Configuration.Authorization.Providers.Add<LoveCardAuthorizationProvider>();
            //Configuration.Authorization.Providers.Add<PlayerLabelAuthorizationProvider>();
            Configuration.Authorization.Providers.Add<OrderAuthorizationProvider>();
            Configuration.Authorization.Providers.Add<MqAgentAuthorizationProvider>();
            Configuration.Authorization.Providers.Add<EnterpirsePaymentRecordAuthorizationProvider>();
            Configuration.Authorization.Providers.Add<ChineseBabyAuthorizationProvider>();

            //映射
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomerBonusPointMapper.CreateMappings);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomerBonusPointRecordMapper.CreateMappings);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomerPlayerMapper.CreateMappings);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomerWeChatMapper.CreateMappings);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(FriendMapper.CreateMappings);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(LoveCardFileMapper.CreateMappings);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(LoveCardOptionMapper.CreateMappings);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(LoveCardMapper.CreateMappings);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(PlayerLabelMapper.CreateMappings);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(OrderMapper.CreateMappings);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(UnlockMapper.CreateMappings);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CompetitionMapper.CreateMappings);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(configuration =>
            {
                TextAudiosMapper.CreateMappings(configuration);
                BabyMapper.CreateMappings(configuration);
                BabyEventRecordMapper.CreateMappings(configuration);
                BabyEventMapper.CreateMappings(configuration);
                CoinRechargeMapper.CreateMappings(configuration);
                InformationMapper.CreateMappings(configuration);
                MqAgentMapper.CreateMappings(configuration);
            });
           
            //修改多租户解析器配置
            Configuration.MultiTenancy.Resolvers.Add<HttpHeaderTenantResolveContributor>();

            Configuration.BackgroundJobs.UseHangfire();
            //关闭审计日志
            Configuration.Auditing.IsEnabled = false;
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(BSMPApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );

            //注入默认配置
            Configuration.Settings.Providers.Add<DefaultSettingProvider>();
        }
        
    }
}

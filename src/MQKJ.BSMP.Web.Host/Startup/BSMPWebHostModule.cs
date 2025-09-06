using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MQKJ.BSMP.Configuration;
using Microsoft.AspNetCore.Cors;
using MQKJ.BSMP.Authentication.External;
using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using MQKJ.BSMP.Common.MqAgents.Agents;

namespace MQKJ.BSMP.Web.Host.Startup
{
    [DependsOn(typeof(BSMPWebCoreModule))]
    public class BSMPWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public BSMPWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BSMPWebHostModule).GetAssembly());
            ConfigureExternalAuthProviders();
        }

        private void ConfigureExternalAuthProviders()
        {
            var externalAuthConfiguration = IocManager.Resolve<ExternalAuthConfiguration>();

            if (bool.Parse(_appConfiguration["Wechat_PublicAccount:IsEnabled"]))
            {
                //externalAuthConfiguration.Providers.Add(
                //    new ExternalLoginProviderInfo(
                //        WechatMiniProgramAuthProviderApi.ProviderNmae,
                //        _appConfiguration["Authentication:WechatMiniProgram:AppId"],
                //        _appConfiguration["Authentication:WechatMiniProgram:Secret"],
                //        typeof(WechatMiniProgramAuthProviderApi)
                //        )
                //    );
                externalAuthConfiguration.Providers.Add(
                    new ExternalLoginProviderInfo(
                        WechatMiniProgramAuthProviderApi.ProviderNmae,
                        _appConfiguration["Wechat_PublicAccount:AppId"],
                        _appConfiguration["Wechat_PublicAccount:Secret"],
                        typeof(WechatMiniProgramAuthProviderApi)
                        )
                    );
            }
        }
        public override void PostInitialize()
        {

            base.PostInitialize();
            var _autoRunnerJob = IocManager.Resolve<IAutoRunnerJob>();
            _autoRunnerJob.ClearAllWorshipJobs( Common.MqAgents.Agents.Config.ClearType.FromStorage);
            _autoRunnerJob.AutoStartWorshipJob();
        }
    }
}

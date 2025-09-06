using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Castle.Facilities.Logging;
using Abp.AspNetCore;
using Abp.Castle.Logging.NLog;
using MQKJ.BSMP.Authentication.JwtBearer;
using MQKJ.BSMP.Configuration;
using MQKJ.BSMP.Identity;
using MQKJ.BSMP.Web.Resources;
using Abp.AspNetCore.SignalR.Hubs;
using Microsoft.EntityFrameworkCore.Design;
using MQKJ.BSMP.EntityFrameworkCore;
using NLog;
using Microsoft.AspNetCore.Mvc.Razor;
using MQKJ.BSMP.BigRisks.WeChat;
using MQKJ.BSMP.BigRisks.WeChat.WechatPay;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MQKJ.BSMP.HttpContextHelper;
using MQKJ.BSMP.QCloud;
using MQKJ.BSMP.Common;
using Microsoft.Extensions.Caching.Redis;
using MQKJ.BSMP.Common.Pay.AliPay;

namespace MQKJ.BSMP.Web.Startup
{
    public class Startup
    {
        private readonly IConfigurationRoot _appConfiguration;

        public Startup(IHostingEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // MVC
            services.AddMvc(
                options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute())
            );

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });

            IdentityRegistrar.Register(services);
            AuthConfigurer.Configure(services, _appConfiguration);


            //配置公众号
            services.Configure<WechatpublicPlatformConfig>(_appConfiguration.GetSection("Wechat_PublicAccount"));
            //配置支付
            services.Configure<WechatPayConfig>(_appConfiguration.GetSection("WechatPay_Config"));
            services.Configure<CloudPayConfig>(_appConfiguration.GetSection("CloudPayConfig"));
            services.Configure<AliPayConfig>(_appConfiguration.GetSection("AliPayConfig"));
            //配置极光短信
            services.Configure<JpushMessageConfig>(_appConfiguration.GetSection("JpushMessage"));

            #region 注入csredis
            var csredis = new CSRedis.CSRedisClient(_appConfiguration.GetConnectionString("Redis"));
            RedisHelper.Initialization(csredis);
            services.AddSingleton(new CSRedisCache(RedisHelper.Instance));
            #endregion

            services.AddHttpClientService();
            services.AddScoped<IQCloudApiClient, QCloudApiClient>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IWebResourceManager, WebResourceManager>();
            services.AddScoped<IDesignTimeDbContextFactory<BSMPDbContext>, MigrationDbContextFactory>();
            services.AddSignalR();
            services.AddWXFramework();
            // Configure Abp and Dependency Injection
            return services.AddAbp<BSMPWebMvcModule>(
                // Configure Log4Net logging
                options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpNLog().WithConfig("nlog.config")
                )
            );
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            app.UseAbp(); // Initializes ABP framework.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            CustomHttpContext.serviceProvider = serviceProvider;

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseJwtTokenMiddleware();

            app.UseSignalR(routes =>
            {
                routes.MapHub<AbpCommonHub>("/signalr");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //配置日志的连接字符串
            LogManager.Configuration.Variables["connectionString"] = _appConfiguration.GetConnectionString("Default");
        }
    }
}

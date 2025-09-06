using Abp.AspNetCore;
using Abp.Castle.Logging.NLog;
using Abp.Extensions;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using MQKJ.BSMP.Configuration;
using MQKJ.BSMP.EntityFrameworkCore;
using MQKJ.BSMP.HttpContextHelper;
using MQKJ.BSMP.Identity;
using MQKJ.BSMP.SwaggerFileUploadFilters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NLog;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Linq;
using System.Reflection;
using Quartz;
using Quartz.Impl;
using MQKJ.BSMP.BigRisks.WeChat;
using MQKJ.BSMP.BigRisks.WeChat.WechatPay;
using Essensoft.AspNetCore.Payment.WeChatPay;
using Hangfire;
using MQKJ.BSMP.Common;
using Microsoft.EntityFrameworkCore.Design;
using MQKJ.BSMP.QCloud;
using MQKJ.BSMP.QCloud.Configs;
using Microsoft.Extensions.Caching.Redis;
using MQKJ.BSMP.Middlewares;
using StackExchange.Profiling.Storage;
using MQKJ.BSMP.Common.WechatPay;

namespace MQKJ.BSMP.Web.Host.Startup
{
    /// <summary>
    /// 启动类
    /// </summary>
    public class Startup
    {
        private const string _defaultCorsPolicyName = "localhost";

        private readonly IConfigurationRoot _appConfiguration;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
        }

        /// <summary>
        /// 注入中间件
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddApiVersioning(v =>
            {
                v.ReportApiVersions = true;//如果为true 会在api请求的响应头部 追加当前的api支持的版本
                v.AssumeDefaultVersionWhenUnspecified = true;//标记当客户端没有制定版本号的时候是否使用默认版本号
                v.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);//默认版本号
                v.ApiVersionReader = new QueryStringApiVersionReader("x-api-version");
            });

            // MVC
            services.AddMvc(
                options => options.Filters.Add(new CorsAuthorizationFilterFactory(_defaultCorsPolicyName))
            ).AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            IdentityRegistrar.Register(services);

            AuthConfigurer.Configure(services, _appConfiguration);

            services.AddOptions();

            //注入微信支付
            //services.AddWeChatPay();

            //配置公众号
            services.Configure<WechatpublicPlatformConfig>(_appConfiguration.GetSection("Wechat_PublicAccount"));
            //配置支付(微信小程序)
            services.Configure<WechatPayConfig>(_appConfiguration.GetSection("WechatPay_Config"));
            services.Configure<CloudPayConfig>(_appConfiguration.GetSection("CloudPayConfig"));
            //配置极光短信
            services.Configure<JpushMessageConfig>(_appConfiguration.GetSection("JpushMessage"));
            services.Configure<QcloudConfig>(_appConfiguration);
            services.Configure<QueryOrderConfig>(_appConfiguration.GetSection("QueryOrderConfig"));

            //配置微信网站信息
            services.Configure<WechatWebConfig>(_appConfiguration.GetSection("WechatWebConfig"));
            services.Configure<WechatPubPayConfig>(_appConfiguration.GetSection("WechatPubPay_Config"));
            services.AddDbContext<BSMPDbContext>(options =>
            {
                options.UseSqlServer(_appConfiguration.GetConnectionString("Default"));
            });

            //增加缓存设置
            //services.AddResponseCaching();

            #region SignalR配置
            var singnalRRedisEnable = Convert.ToBoolean(_appConfiguration.GetSection("SingnalRRedisEnable").Value);

            var singalR = services.AddSignalR(options =>
              {
                  options.KeepAliveInterval = TimeSpan.FromSeconds(5);//心跳包间隔

                  options.EnableDetailedErrors = true;

              }).AddJsonProtocol(options =>  //用于配置序列化的参数和返回值的对象
              {
                  options.PayloadSerializerSettings.Converters.Add(new IsoDateTimeConverter());

                  options.PayloadSerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Unspecified;

                  options.PayloadSerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
              });
            if (singnalRRedisEnable)
            {
                //负载均衡
                //singalR.AddRedis(options =>
                //  {
                //      options.Configuration.ConnectTimeout = 30;
                //      options.Configuration.EndPoints.Add(_appConfiguration.GetConnectionString("Redis"));

                //  });
                //存储
                //services.AddDistributedRedisCache(options =>
                //{
                //    //用于连接Redis的配置  读取配置信息的串
                //    options.Configuration = _appConfiguration.GetConnectionString("Redis");
                //    options.InstanceName = "_mqbsmp_";
                //});
            }

            #endregion

            // Configure CORS for angular2 UI
            services.AddCors(
                options => options.AddPolicy(
                    _defaultCorsPolicyName,
                    builder => builder
                        .WithOrigins(
                            // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                            _appConfiguration["App:CorsOrigins"]
                                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                    .Select(o => o.RemovePostFix("/"))
                                    .ToArray()
                            )
                            .AllowAnyHeader()
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowCredentials()
                    )
                );

            #region 注入csredis
            var csredis = new CSRedis.CSRedisClient(_appConfiguration.GetConnectionString("Redis"));
            RedisHelper.Initialization(csredis);
            services.AddSingleton(new CSRedisCache(RedisHelper.Instance));
            #endregion

            // Swagger - Enable this line and the related lines in Configure method to enable swagger UI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "BSMP API", Version = "v1" });
                options.SwaggerDoc("chinesebaby", new Info { Title = "Chinese Baby API v1", Version = "v1" });
                options.SwaggerDoc("appservices", new Info { Title = "Application Service", Version = "appservices" });
                options.SwaggerDoc("agents", new Info { Title = "Agent Service", Version = "agents" });
                options.DocInclusionPredicate((docName, description) =>
                {
                    if (docName == "v1")
                    {
                        return description.GroupName == "v1" || String.IsNullOrEmpty(description.GroupName);
                    }

                    if (docName == "appservices")
                    {
                        return description.RelativePath.Contains("api/services/app");
                    }

                    return description.GroupName == docName;
                });

                //Define the BearerAuth scheme that's in use
                options.AddSecurityDefinition("bearerAuth", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                // Assign scope requirements to operations based on AuthorizeAttribute
                //options.OperationFilter<SecurityRequirementsOperationFilter>();
                //options.OperationFilter<SwaggerFileUploadFilter>();
            });
            //BSMPDbContextFactory s = new BSMPDbContextFactory();
            //services.AddSingleton(s);
            services.Configure<WeChatPayOptions>(_appConfiguration.GetSection("WechatPay_Config"));

            //自定义httpcontext
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //使用quartz
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            //增加HangFire的依赖注入
            //var temp = _appConfiguration.GetConnectionString("Default");
            services.AddHangfire(config =>
            {
                config.UseSqlServerStorage(_appConfiguration.GetConnectionString("Hangfire"));
            });
            services.AddSingleton<IBackgroundJobClient, BackgroundJobClient>();

            services.AddWXFramework();
            services.AddHttpClientService();
            services.AddScoped<IQCloudApiClient, QCloudApiClient>();

            //注入sql监测工具 MiniProfiler
            services.AddMiniProfiler(options => options.RouteBasePath = "/profiler");

            // Configure Abp and Dependency Injection
            return services.AddAbp<BSMPWebHostModule>(
                // Configure Log4Net logging
                options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpNLog().WithConfig("nlog.config")
                )
            );

        }


        /// <summary>
        /// 配置使用中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="serviceProvider"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            LogManager.Configuration.Variables["connectionString"] = _appConfiguration.GetConnectionString("Default");
            //添加对UseHangfireServer的调用。
            app.UseHangfireServer();
            app.UseHangfireDashboard();
            app.UseAbp(options => { options.UseAbpRequestLocalization = false; }); // Initializes ABP framework.

            app.UseCors(_defaultCorsPolicyName); // Enable CORS!

            CustomHttpContext.serviceProvider = serviceProvider;

            app.UseStaticFiles();
            //实现缓存设置
            //app.UseResponseCaching();

            app.UseAuthentication();

            app.UseAbpRequestLocalization();

            app.UseSignalR(routes =>
            {
                //设置SignalR 路由以及 sec-websocket-protocol自定义值的标头
                //routes.MapHub<AbpCommonHub>("/signalr", options => options.WebSockets.SubProtocolSelector = requestedProtocols =>
                //{
                //    return requestedProtocols.Count > 0 ? requestedProtocols[0] : null;
                //});
                //routes.MapHub<SignalRChat.WeChat>("/chat", options => options.WebSockets.SubProtocolSelector = requestedProtocols =>
                //{
                //    return requestedProtocols.Count > 0 ? requestedProtocols[0] : null;
                //});
                routes.MapHub<SignalRChat.WeChat>("/chat");
            });
            // 一定要放到UseMVC前
            //app.UseMiddleware(typeof(ResponseTimeMiddleware));

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "defaultWithArea",
                    template: "{area}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });
            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "BSMP API V1");
                options.SwaggerEndpoint("/swagger/chinesebaby/swagger.json", "Chinese Baby Api");
                options.SwaggerEndpoint("/swagger/appservices/swagger.json", "Application Services");
                options.SwaggerEndpoint("/swagger/agents/swagger.json", "Agents Services");
                options.IndexStream = () => Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("MQKJ.BSMP.Web.Host.wwwroot.swagger.ui.index.html");
            }); // URL: /swagger
            //配置日志的连接字符串
            LogManager.Configuration.Variables["connectionString"] = _appConfiguration.GetConnectionString("Default");


            app.UseMiniProfiler();
        }
    }
}

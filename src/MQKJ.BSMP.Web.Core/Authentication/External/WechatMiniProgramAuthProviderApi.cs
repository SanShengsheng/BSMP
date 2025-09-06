using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Castle.Core.Logging;
using Newtonsoft.Json;
using MQKJ.BSMP.MiniappServices;
using MQKJ.BSMP.Players.WeChat;

namespace MQKJ.BSMP.Authentication.External
{
    public class WechatMiniProgramAuthProviderApi : ExternalAuthProviderApiBase
    {
        public const string ProviderNmae = "WeChatProvider";

        WeChatOptions _options;
        private readonly IExternalAuthConfiguration _externalAuthConfiguration;
        private readonly ILogger logger;
        private readonly IMiniappService _miniappService;

        private readonly IWeChatPlayerAppService _weChatPlayerAppService;

        public WechatMiniProgramAuthProviderApi(IExternalAuthConfiguration externalAuthConfiguration,
            ILogger logger,
            IMiniappService miniappService,
            IWeChatPlayerAppService weChatPlayerAppService
            )
        {
            _externalAuthConfiguration = externalAuthConfiguration;
            var r = externalAuthConfiguration.Providers.First(p => p.Name == ProviderNmae);
            _options = new WeChatOptions
            {
                AppId = r.ClientId,
                Secret = r.ClientSecret
            };

            this.logger = logger;

            _miniappService = miniappService;

            _weChatPlayerAppService = weChatPlayerAppService;
        }

        public override async Task<ExternalAuthUserInfo> GetUserInfoAsync(string accessCode)
        {

            WeChatSession wechat = new WeChatSession();
            var output = await _weChatPlayerAppService.V2_VaildPubPlayer(new WeChat.Dtos.VaildPubPlayerInput
            {
                Code = accessCode
            });

            var t = wechat == null ? new ExternalAuthUserInfo() : new ExternalAuthUserInfo
            {
                EmailAddress = output.OpenId + "@wechat.cn",
                Surname = output.UnionId, //unionid
                ProviderKey = output.OpenId,//openid
                Provider = ProviderNmae,
                Name = output.NickName,
                HeadUrl = output.HeadUrl,
                PlayerId = output.PlayerId,
                AgentId = output.AgentId
            };
            return t;
        }
    }

    public class WeChatSession
    {
        public string OpenId { get; set; }

        public string UnionId { get; set; }

        public string NickName { get; set; }

        public string HeadUrl { get; set; }
    }

    public class WeChatOptions
    {
        public string AppId { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string Secret { get; set; }
    }
}

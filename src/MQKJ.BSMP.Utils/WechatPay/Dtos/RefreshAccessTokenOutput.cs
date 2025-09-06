using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Utils.WechatPay.Dtos
{
    public class RefreshAccessTokenOutput
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public string ExpireTime { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("openid")]
        public string OpenId { get; set; }
    }
}

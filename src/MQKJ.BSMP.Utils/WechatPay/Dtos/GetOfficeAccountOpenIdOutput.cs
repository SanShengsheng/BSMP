using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MQKJ.BSMP.Utils.WechatPay.Dtos
{
    public class GetOfficeAccountOpenIdOutput
    {
        /// <summary>
        /// 网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// access_token接口调用凭证超时时间，单位（秒）
        /// </summary>
        [JsonProperty("expires_in")]
        public string ExpiresTime { get; set; }

        /// <summary>
        /// 用户刷新access_token
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// 用户唯一标识
        /// </summary>
        [JsonProperty("openid")]
        public string OpenId { get; set; }

        /// <summary>
        /// 用户授权的作用域，使用逗号（,）分隔
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; }


        [JsonProperty("errcode")]
        public string ErrCode { get; set; }

        [JsonProperty("errmsg")]
        public string ErrMsg { get; set; }
    }
}

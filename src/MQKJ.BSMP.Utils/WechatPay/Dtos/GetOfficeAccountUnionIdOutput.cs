using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Utils.WechatPay.Dtos
{
    public class GetOfficeAccountUnionIdOutput
    {
        [JsonProperty("openid")]
        public string OpenId { get; set; }

        [JsonProperty("nickname")]
        public string NickName { get; set; }

        /// <summary>
        /// 1-男2-女
        /// </summary>
        [JsonProperty("sex")]
        public virtual int Gender { get; set; }

        [JsonProperty("province")]
        public string Province { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }


        [JsonProperty("headimgurl")]
        public virtual string HeadUrl { get; set; }

        [JsonProperty("privilege")]
        public string[] Privilege { get; set; }

        [JsonProperty("unionid")]
        public string UnionId { get; set; }

        [JsonProperty("errcode")]
        public string ErrCode { get; set; }

        [JsonProperty("errmsg")]
        public string ErrMsg { get; set; }
    }
}

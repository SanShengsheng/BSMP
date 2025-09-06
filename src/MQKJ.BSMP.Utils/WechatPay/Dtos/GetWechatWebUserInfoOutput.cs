using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Utils.WechatPay.Dtos
{
    public class GetWechatWebUserInfoOutput
    {
        [JsonProperty("openid")]
        public string OpenId { get; set; }


        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("sex")]
        public int Gender { get; set; }

        [JsonProperty("province")]
        public string Province { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("headimgurl")]
        public string Headimgurl { get; set; }

        //public string Privilege { get; set; }

        [JsonProperty("unionid")]
        public string unionid { get; set; }
    }
}

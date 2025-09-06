using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Utils.WechatPay.Dtos
{
    public class CheckAccessTokenOutput
    {

        [JsonProperty("errcode")]
        public string ErrCode { get; set; }

        [JsonProperty("errmsg")]
        public string ErrMsg { get; set; }

        public bool IsExpire()
        {
            return ErrCode == "0" && ErrMsg == "ok" ? true : false;
        }
    }
}

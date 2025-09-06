using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Utils.WechatPay.Dtos
{
    public class GetWechatWebUserInfoInput
    {
        public string AccessToken { get; set; }

        public string OpenId { get; set; }

        public string Lang { get; set; } = "zh_CN";

    }
}

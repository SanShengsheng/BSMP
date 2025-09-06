using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Utils.WechatPay.Dtos
{
    public class GetAccessTokenWithCodeInput
    {
        public string AppId { get; set; }

        public string Secret { get; set; }


        public string Code { get; set; }

        public string GrantType { get; set; } = "authorization_code";
    }
}

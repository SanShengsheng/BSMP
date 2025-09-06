using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Utils.WechatPay.Dtos
{
    public class RefreshAccessTokenInput
    {
        public string AppId { get; set; }

        /// <summary>
        /// 填写refreshtoken
        /// </summary>
        public string GrantType { get; set; }

        public string RefreshToken { get; set; }
    }
}

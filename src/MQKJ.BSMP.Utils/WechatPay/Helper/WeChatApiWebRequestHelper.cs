using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Utils.WechatPay.Helper
{
    public  class WeChatApiWebRequestHelper: WebRequestHelper
    {
        public WeChatApiWebRequestHelper()
        {
            UserAgent =
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
            ContentType = "application/json";
            AcceptLanguage = "zh-cn";
        }
    }
}

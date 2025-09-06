using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.BigRisks.WeChat
{
    public class WechatWebConfig
    {
        public string AppId { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string Secret { get; set; }

        public string Grant_Type { get; set; }

        /// <summary>
        /// IsEnabled
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}

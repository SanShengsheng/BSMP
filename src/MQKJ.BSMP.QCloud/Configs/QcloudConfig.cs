using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.QCloud.Configs
{
    public class QcloudConfig
    {
        /// <summary>
        /// 是否内网访问
        /// </summary>
        public bool IsPrivate { get; set; }
        /// <summary>
        /// qcloud 的appid
        /// </summary>
        public string QcloudAppId { get; set; }
        /// <summary>
        /// qcloud 的 secret
        /// </summary>
        public string QcouldAppSecret { get; set; }

        public CMQConfig CmqConfig { get; set; }
        public string MqApiUrl { get; set; }
        public string MQ_WechatPay { get; set; }
    }
}

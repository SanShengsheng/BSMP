using System;

namespace MQKJ.BSMP.Utils.Message.Dtos
{
    public class WeChatAccessToken
    {
        /// <summary>
        /// 获取到的凭证
        /// </summary>
        public string Access_token { get; set; }

        /// <summary>
        /// 凭证有效时间
        /// </summary>
        public int Expires_in { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }
    }
}

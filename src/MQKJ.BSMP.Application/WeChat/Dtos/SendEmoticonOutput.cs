using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class SendEmoticonOutput
    {
        /// <summary>
        /// 使用道具方
        /// </summary>
        [JsonIgnore]
        public string UserConnectionId { get; set; }

        /// <summary>
        /// 对方
        /// </summary>
        [JsonIgnore]
        public string OtherConnectionId { get; set; }

        /// <summary>
        /// 表情編碼
        /// </summary>
        public int EmoticonCode { get; set; }

        public Guid PlayerId { get; set; }
    }
}

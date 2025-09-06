using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class OtherAgreeOrRefuseOutput
    {
        [JsonIgnore]
        /// <summary>
        /// 对方的Id
        /// </summary>
        public Guid OtherPlayerId { get; set; }


        /// <summary>
        /// 游戏的ID
        /// </summary>
        public Guid GameId { get; set; }
    }
}

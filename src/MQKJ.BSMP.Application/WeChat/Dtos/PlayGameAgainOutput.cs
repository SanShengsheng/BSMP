using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace MQKJ.BSMP.WeChat.Dtos
{
    public class PlayGameAgainOutput
    {



        [JsonIgnore]
        /// <summary>
        /// 对方的连接Id
        /// </summary>
        public string OtherConnectionId { get; set; }

        //[JsonIgnore]
        /// <summary>
        /// 连接Id
        /// </summary>
        //public string ConnectionId { get; set; }

        /// <summary>
        /// 自己的Id
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// 游戏Id
        /// </summary>
        public Guid GameId { get; set; }

    }
}

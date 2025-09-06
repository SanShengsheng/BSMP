using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class GetGameRoomStateInput
    {
        /// <summary>
        /// 游戏编号
        /// </summary>
        public Guid GameId { get; set; }

        /// <summary>
        /// 玩家id
        /// </summary>
        public Guid PlayerId { get; set; }
    }
}

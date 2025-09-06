using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class OtherAgreeOrRefuseDto
    {
        /// <summary>
        /// 原来游戏Id
        /// </summary>
        public Guid OldGameId { get; set; }

        /// <summary>
        /// 游戏Id
        /// </summary>
        public Guid GameId { get; set; }

        /// <summary>
        /// 玩家Id
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// 另一方id
        /// </summary>
        public Guid OtherPlayerId { get; set; }
    }
}

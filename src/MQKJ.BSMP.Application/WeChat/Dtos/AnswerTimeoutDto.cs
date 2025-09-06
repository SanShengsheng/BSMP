using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class AnswerTimeoutDto
    {
        /// <summary>
        /// 房间Id
        /// </summary>
        public Guid GameId { get; set; }

        /// <summary>
        /// 玩家Id
        /// </summary>
        public Guid PlayerId { get; set; }
    }
}

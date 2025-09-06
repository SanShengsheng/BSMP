using MQKJ.BSMP.GameTasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class SubmitAnswerInputDto
    {
        /// <summary>
        /// 游戏编号
        /// </summary>
        public Guid GameId { get; set; }
        /// <summary>
        /// 问题编号
        /// </summary>
        public int QuestionId { get; set; }
        /// <summary>
        /// 选项编号
        /// </summary>
        public int AnswerId { get; set; }
        /// <summary>
        /// 玩家编号
        /// </summary>
        public Guid PlayerId { get; set; }

        public Guid InviteePlayerId { get; set; }

        public int OtherAnswerId { get; set; }

        public GameType GameType { get; set; }

        public int Sort { get; set; }
     

    }
}

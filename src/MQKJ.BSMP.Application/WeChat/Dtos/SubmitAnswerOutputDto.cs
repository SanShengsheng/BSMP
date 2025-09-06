using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MQKJ.BSMP.WeChat.Dtos
{
public    class SubmitAnswerOutputDto
    {
        public int Sort { get; set; }
        /// <summary>
        /// 提交答案的玩家编号
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// 另一方的id
        /// </summary>
        public Guid OtherPlayerId { get; set; }

        public int Floor { get; set; }

        /// <summary>
        /// 体力
        /// </summary>
        public int Stamina { get; set; }
        /// <summary>
        /// 是否作弊
        /// </summary>
        public bool IsCheat { get; set; }

        public int InviterSort { get; set; }

        public int InviteeSort { get; set; }
    }
}

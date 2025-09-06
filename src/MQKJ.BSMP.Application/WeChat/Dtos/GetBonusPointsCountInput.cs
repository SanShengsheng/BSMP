using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class GetBonusPointsCountInput
    {
        public Guid playerId { get; set; }

        public Guid GameId { get; set; }

        public int QuestionId { get; set; }

        //public int OptionA { get; set; }

        //public int OptionB { get; set; }

        //public int OptionC { get; set; }
    }
}

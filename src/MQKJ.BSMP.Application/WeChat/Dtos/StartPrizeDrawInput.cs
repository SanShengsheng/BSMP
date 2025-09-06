using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class StartPrizeDrawInput
    {
        public Guid GameId { get; set; }


        public Guid PlayerId { get; set; }
    }
}

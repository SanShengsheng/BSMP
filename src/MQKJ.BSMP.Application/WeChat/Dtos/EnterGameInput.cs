using MQKJ.BSMP.GameTasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class EnterGameInput
    {
        public Guid GameId { get; set; }

        public Guid PlayerId { get; set; }

        public GameType GameType { get; set; }
    }
}

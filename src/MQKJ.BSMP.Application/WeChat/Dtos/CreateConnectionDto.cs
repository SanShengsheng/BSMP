using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Players.WeChat.Dtos
{
    public class CreateConnectionDto
    {
        public string ConnectionId { get; set; }

        public Guid PlayerId { get; set; }

        public Guid GameId { get; set; }
    }
}

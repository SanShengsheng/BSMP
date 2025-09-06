using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class GetPosterInfoInput
    {
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }

        public Guid FriendId { get; set; }
    }
}

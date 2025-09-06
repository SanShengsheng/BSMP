using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.WeChat.Dtos
{
    public class VaildPubPlayerOutput
    {
        public Guid PlayerId { get; set; }

        public string NickName { get; set; }

        public string HeadUrl { get; set; }

        public string OpenId { get; set; }

        public string UnionId { get; set; }

        public int? AgentId { get; set; }
    }
}
